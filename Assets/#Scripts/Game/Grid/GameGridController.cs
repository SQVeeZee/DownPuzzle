using UnityEngine;
using System;

public class GameGridController : MonoBehaviour
{
    [Header("Grid")]
    [SerializeField] private GridSize _gridSize = default;
    [SerializeField] private CellsLocalPosition _gridPosition = default;
    [SerializeField] private Vector2 _gridScreenSize = default;

    [Header("Prefabs")]
    [SerializeField] private CellController _cellController = null;

    [Header("Transform")]
    [SerializeField] private Transform _cellsTransform = null;

    [Header("GridCellsController")]
    [SerializeField] private GridCellsController _gridCellsController = null;

    private float _cellSize = 1;

    private float _clickDistance => _cellSize / 2;

    private GridCell[,] _gridsCell;
    
    private CellsColorController _cellsColorController = null;
    private MobileInputController _mobileInputController = null;
    private Camera _gameCamera = null;

    private void Awake()
    {
        Initialize();
    }
    private void OnEnable()
    {
        Subscribe();
    }
    private void OnDisable()
    {
        UnSubscribe();
    }

    public void Initialize()
    {
        _cellsColorController = CellsColorController.Instance;
        _mobileInputController = MobileInputController.Instance;

        _gameCamera = CameraManager.Instance.GetCameraItem(ECameraType.GAME).Camera;
    }

    public void CreateAndInitializeGrid()
    {
        _gridsCell = new GridCell[_gridSize.Width,_gridSize.Height];
        
        Vector2 firstGlobalCellPosition = GetStartCellsGlobalPosition();

        for (int i = 0; i < _gridSize.Height; i++)
        {
            for (int j = 0; j < _gridSize.Width; j++)
            {
                CellsLocalPosition cellsLocalPosition = GetGridPosition(i, j);

                var globalCellPosition = firstGlobalCellPosition + new Vector2(j, i);

                GridCell gridCell = GetNewFilledGridCell(globalCellPosition, cellsLocalPosition);

                var cellController = InstantiateCell(gridCell);
                cellController.SetCellColor(_cellsColorController.GetRandomColorGroup());

                gridCell.CellController = cellController;

                _gridsCell[i, j] = gridCell;
            }
        }

        SetCellsNeighbors();
    }

    private void SetCellsNeighbors()
    {
        for (int i = 0; i < _gridSize.Height; i++)
        {
            for (int j = 0; j < _gridSize.Width; j++)
            {
                SetCellsNeighbors(_gridsCell[i, j]);
            }
        }
    }


    private void SetCellsNeighbors(GridCell gridCell)
    {
        Direction direction = new Direction();
        
        foreach (EDirectionType directionType in (EDirectionType[]) Enum.GetValues(typeof(EDirectionType)))
        {
            if(directionType == EDirectionType.NONE) continue;

            direction.DirectionType = directionType;

            direction.TryGetGridLocalPositionByDirectionType(gridCell.CellsLocalPosition, directionType, OnGetCellsLocalPosition);

            void OnGetCellsLocalPosition(CellsLocalPosition cellsLocalPosition)
            {
                var gridCellInDirection = GetElementByGridPosition(cellsLocalPosition);
            
                if(gridCellInDirection != null)
                {
                    gridCell.ElementsInDirection.Add(directionType, gridCellInDirection);
                }
            }
        }
    }

    private CellsLocalPosition GetGridPosition(int i, int j)
    {
        CellsLocalPosition cellsLocalPosition = new CellsLocalPosition();

        cellsLocalPosition.PositionX = i;
        cellsLocalPosition.PositionY = j;

        return cellsLocalPosition;
    }

    private GridCell GetElementByGridPosition(CellsLocalPosition cellsLocalPosition)
    {
        if(!IsGridCellExists(cellsLocalPosition)) return null;

        GridCell gridCell = _gridsCell[cellsLocalPosition.PositionX, cellsLocalPosition.PositionY];
        
        if(gridCell != null)
        {
            return gridCell;
        }

        return null;
    }

    private bool IsGridCellExists(CellsLocalPosition cellsLocalPosition)
    {
        if(cellsLocalPosition.PositionX >= _gridSize.Width || cellsLocalPosition.PositionX < 0 || cellsLocalPosition.PositionY >= _gridSize.Height || cellsLocalPosition.PositionY < 0)
        {
            return false;
        }

        return true;
    }

    private CellController InstantiateCell(GridCell gridCell)
    {
        CellController cellController = Instantiate(_cellController, _cellsTransform);

        cellController.CellTransform.position = gridCell.GlobalPosition;

        return cellController;
    }

    private Vector2 GetStartCellsGlobalPosition() => new Vector2(_gridPosition.PositionX, _gridPosition.PositionY) - new Vector2(_gridSize.Width - 1, _gridSize.Height - 1) / 2;

    private GridCell GetNewFilledGridCell(Vector2 globalPosition, CellsLocalPosition cellsLocalPosition)
    {
        GridCell newGridCell = new GridCell();
        
        newGridCell.CellType = ECellType.FILL;
        newGridCell.GlobalPosition = globalPosition;
        newGridCell.CellsLocalPosition = cellsLocalPosition;

        return newGridCell;
    }

    private void OnPointerDown(Vector2 inputPosition)
    {
        var worldPosition = (Vector2)_gameCamera.ScreenToWorldPoint(inputPosition);

        var cell = GetGridCellByPosition(worldPosition);
        
        if (cell != null && cell.CellController != null)
        {
            OnClickCell(cell);
        }
    }

    private void OnClickCell(GridCell clickedGridCell)
    {
        _gridCellsController.OnClickCell(clickedGridCell);
    }

    private GridCell GetGridCellByPosition(Vector2 worldPosition)
    {
        for(int i = 0; i< _gridSize.Width;i++)
        {
            for(int j = 0; j < _gridSize.Height; j++)
            {
                if(Vector3.Distance(_gridsCell[i,j].GlobalPosition, worldPosition) < _clickDistance)
                {
                    return _gridsCell[i,j];
                }
            }
        }

        return null;
    }

    private void Subscribe()
    {
        _mobileInputController.onPointerDown += OnPointerDown;
    }
    private void UnSubscribe()
    {
        _mobileInputController.onPointerDown -= OnPointerDown;
    }
}
