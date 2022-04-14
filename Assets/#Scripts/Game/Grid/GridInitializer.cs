using System;
using UnityEngine;

public class GridInitializer : MonoBehaviour
{
    public event Action<Vector2[,]> onSetCorners;

    [Header("Cells")]
    [SerializeField] private CellController _cellControllerPrefab = null;
    [SerializeField] private Transform _cellsTransform = null;

    private CellsColorController _cellsColorController = null;
    private GridConfigs _gridConfigs = null;
    private GridBorderConfigs _borderConfigs = null;

    private float _cellSizeMultiplayer = 1;
    private Vector2 _cellSize => Vector3.one * _cellSizeMultiplayer;

    private int _width = 0;
    private int _height = 0;

    private float _clampX => _gridConfigs.СlampX;
    private float _minClampY => _gridConfigs.MinClampY;
    private float _maxClampY => _gridConfigs.MaxClampY;

    private Camera _gameCamera = null;

    public Vector2 CellSize => _cellSize;

    public void Initialize(GridConfigs gridConfigs, GridBorderConfigs borderConfigs, GridSize gridSize)
    {
        _cellsColorController = CellsColorController.Instance;
        _gameCamera = CameraManager.Instance.GetCameraItem(ECameraType.GAME).Camera;

        _gridConfigs = gridConfigs;
        _borderConfigs = borderConfigs;

        _width = gridSize.Width;
        _height = gridSize.Height;
    }

    public GridCell[,] CreateAndInitializeGrid(EVerticalDirectionType verticalDirectionType, EHorizontalDirectionType horizontalDirectionType)
    {
        if( _width == 0 || _height == 0)
        {
            Debug.LogError("Levels Grid Size is wrong");
            
            return null;
        }
        _cellSizeMultiplayer = GetCellSizeMultiplayer();

        GridCell[,] gridCells = new GridCell[_width, _height];
        
        Vector2[,] extremePoints = DefineExtremeWorldPoints();

        onSetCorners?.Invoke(extremePoints);

        Vector2 firstGlobalCellPosition = GetFirstElement(extremePoints, verticalDirectionType, horizontalDirectionType);
        
        int verticalIncreaser = GetSignForVerticalMove(verticalDirectionType);
        int horizontalIncreaser = GetSignForHorizontalMove(horizontalDirectionType);
        
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                CellsLocalPosition cellsLocalPosition = GetCellsLocalPosition(i, j);

                var globalCellPosition = firstGlobalCellPosition + new Vector2(i * horizontalIncreaser, j * verticalIncreaser) * _cellSize;

                GridCell gridCell = GetNewFilledGridCell(globalCellPosition, cellsLocalPosition);

                CellController cellController = InstantiateCell(gridCell);

                cellController.SetCellColor(_cellsColorController.GetRandomColorGroup());

                gridCells[i, j] = gridCell;
            }
        }

        SetCellsNeighbors(gridCells);

        return gridCells;
    }

    public void SetCellsNeighbors(GridCell[,] gridCells)
    {
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                SetCellsNeighbors(gridCells, gridCells[i, j]);
            }
        }
    }

    private int GetSignForHorizontalMove(EHorizontalDirectionType horizontalDirectionType)
    {
        switch(horizontalDirectionType)
        {
            case EHorizontalDirectionType.LEFT: return 1;
            case EHorizontalDirectionType.RIGHT: return -1;
        }

        return 0;
    }
    private int GetSignForVerticalMove(EVerticalDirectionType verticalDirectionType)
    {
        switch(verticalDirectionType)
        {
            case EVerticalDirectionType.DOWN: return 1;
            case EVerticalDirectionType.TOP: return -1;
        }

        return 0;
    }

    private void SetCellsNeighbors(GridCell[,] gridCells, GridCell gridCell)
    {
        gridCell.ElementsInDirection.Clear();

        Direction direction = new Direction();
        
        foreach (EDirectionType directionType in (EDirectionType[]) Enum.GetValues(typeof(EDirectionType)))
        {
            if(directionType == EDirectionType.NONE) continue;

            direction.DirectionType = directionType;

            direction.TryGetGridLocalPositionByDirectionType(gridCell.CellsLocalPosition, directionType, OnGetCellsLocalPosition);

            void OnGetCellsLocalPosition(CellsLocalPosition cellsLocalPosition)
            {
                var gridCellInDirection = GetCellByLocalPosition(gridCells, cellsLocalPosition);
            
                if(gridCellInDirection != null)
                {
                    gridCell.ElementsInDirection.Add(directionType, gridCellInDirection);
                }
            }
        }
    }
    
    private CellController InstantiateCell(GridCell gridCell)
    {
        CellController cellController = Instantiate(_cellControllerPrefab, _cellsTransform);

        var cellTransform = cellController.CellTransform;

        cellTransform.localScale = _cellSize;

        cellTransform.position = gridCell.GlobalPosition;

        gridCell.CellController = cellController;

        return cellController;
    }

    private GridCell GetNewFilledGridCell(Vector2 globalPosition, CellsLocalPosition cellsLocalPosition)
    {
        GridCell newGridCell = new GridCell();
        
        newGridCell.CellType = ECellType.FILL;
        newGridCell.GlobalPosition = globalPosition;
        newGridCell.CellsLocalPosition = cellsLocalPosition;

        return newGridCell;
    }

    private GridCell GetCellByLocalPosition(GridCell[,] gridCells, CellsLocalPosition cellsLocalPosition)
    {
        if(!IsGridCellExists(cellsLocalPosition)) return null;

        GridCell gridCell = gridCells[cellsLocalPosition.PositionX, cellsLocalPosition.PositionY];
        
        if(gridCell != null)
        {
            return gridCell;
        }

        return null;
    }

    private bool IsGridCellExists(CellsLocalPosition cellsLocalPosition)
    {
        if(cellsLocalPosition.PositionX >= _width || cellsLocalPosition.PositionX < 0 || cellsLocalPosition.PositionY >= _height || cellsLocalPosition.PositionY < 0)
        {
            return false;
        }

        return true;
    }

    private CellsLocalPosition GetCellsLocalPosition(int i, int j)
    {
        CellsLocalPosition cellsLocalPosition = new CellsLocalPosition();

        cellsLocalPosition.PositionX = i;
        cellsLocalPosition.PositionY = j;

        return cellsLocalPosition;
    }

    private Vector2 GetFirstElement(Vector2[,] extremePoints, EVerticalDirectionType verticalDirectionType, EHorizontalDirectionType horizontalDirectionType)
    {
        Vector2 displacement = CellSize / 2;

        if(verticalDirectionType == EVerticalDirectionType.DOWN && horizontalDirectionType == EHorizontalDirectionType.LEFT)
        {
            return extremePoints[0, 0] + displacement;
        }
        if(verticalDirectionType == EVerticalDirectionType.DOWN && horizontalDirectionType == EHorizontalDirectionType.RIGHT)
        {
            return extremePoints[1, 0] + new Vector2(-displacement.x, displacement.y);
        }
        if(verticalDirectionType == EVerticalDirectionType.TOP && horizontalDirectionType == EHorizontalDirectionType.LEFT)
        {
            return extremePoints[0, 1] + new Vector2(displacement.x, -displacement.y);
        }
        if(verticalDirectionType == EVerticalDirectionType.TOP && horizontalDirectionType == EHorizontalDirectionType.RIGHT)
        {
            return extremePoints[1, 1] + new Vector2(-displacement.x, -displacement.y);;
        }

        return Vector2.zero;
    }

    private float GetCellSizeMultiplayer()
    {
        float rightWorldPos = ConvertScreenToWorldPoint(Vector2.one * _rightScreenPos).x;
        float topWorldPos = ConvertScreenToWorldPoint(Vector2.one * Mathf.Abs(_topScreenPos)).y;
        float bottomWorldPos = ConvertScreenToWorldPoint(Vector2.one * Mathf.Abs(_bottomScreenPos)).y;

        float widthDistance =  rightWorldPos * 2;
        float heightDistance = topWorldPos - bottomWorldPos;

        float cellSizeByWidth = widthDistance / _width;
        float cellSizeByHeight = heightDistance / _height;

        float cellSizeMultiplayer = 0;

        if(cellSizeByWidth < cellSizeByHeight)
        {
            cellSizeMultiplayer = cellSizeByWidth;
        }
        else
        {
            cellSizeMultiplayer = cellSizeByHeight;
        }

        return cellSizeMultiplayer;
    }

    float _screenWidth = Screen.width;
    float _screenHeight = Screen.height;

    float _leftScreenPos => _screenWidth * _clampX;
    float _rightScreenPos => _screenWidth - _screenWidth * _clampX;

    float _bottomScreenPos => _screenHeight * _minClampY;
    float _topScreenPos => _screenHeight * _maxClampY;

    private Vector2[,] DefineExtremeWorldPoints()
    {
        Vector2[,] extremeScreenPoints = new Vector2[2, 2];

        float topWorldPos = ConvertScreenToWorldPoint(Vector2.one * Mathf.Abs(_topScreenPos)).y;
        float bottomWorldPos = ConvertScreenToWorldPoint(Vector2.one * Mathf.Abs(_bottomScreenPos)).y;

        float middleVerticalPoint = (topWorldPos + bottomWorldPos) / 2;

        float leftScreenPos = -(((float)_width) / 2) * _cellSizeMultiplayer;
        float rightScreenPos = leftScreenPos + _width * _cellSizeMultiplayer;

        float bottomScreenPos = (middleVerticalPoint - (((float)_height + 1) / 2) * _cellSizeMultiplayer);
        float topScreenPos = bottomScreenPos + _height * _cellSizeMultiplayer;

        extremeScreenPoints[0, 0] = GetExtremePoint(leftScreenPos, bottomScreenPos);
        extremeScreenPoints[1, 0] = GetExtremePoint(rightScreenPos, bottomScreenPos);
        extremeScreenPoints[1, 1] = GetExtremePoint(rightScreenPos, topScreenPos);
        extremeScreenPoints[0, 1] = GetExtremePoint(leftScreenPos, topScreenPos);

        return extremeScreenPoints;
    }

    private Vector2 ConvertScreenToWorldPoint(Vector2 screenPoint)
    {
        Vector2 worldPoint = _gameCamera.ScreenToWorldPoint(screenPoint);

        return worldPoint;
    }

    private Vector2 GetExtremePoint(float horizontalPos, float verticalPos)
    {
        Vector2 extremePoint = new Vector2();

        extremePoint.x = horizontalPos;
        extremePoint.y = verticalPos;

        return extremePoint;
    }
}
