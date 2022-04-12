using System.IO;
using System;
using UnityEngine;
using System.Collections.Generic;

public class LevelItem : MonoBehaviour
{
    public event Action<ELevelCompleteReason> onLevelComplete = null;

    [Header("MoveType")]
    [SerializeField] private EVerticalDirectionType _verticalDirectionType;
    [SerializeField] private EHorizontalDirectionType _horizontalDirectionType;

    [Header("Grid")]
    [SerializeField] private GridInitializer _gridInitializer = null;
    [SerializeField] private Border _border = null;

    [Header("Cells")]
    [SerializeField] private GridCellsController _gridCellsController = null;

    private LevelItemConfigs _levelItemConfigs = null;
    private MobileInputController _mobileInputController = null;
    private GridController _gridController = null;
    private Camera _gameCamera = null;
    private GridConfigs _gridConfigs => _gridController.GridConfigs;
    private GridBorderConfigs _borderConfigs => _gridController.BorderConfigs;

    private GridSize _gridSize => _levelItemConfigs.GridSize;
    private GridCell[,] _gridCells;
    private float _clickDistance => _gridInitializer.CellSize.x / 2;

    private void Awake()
    {
        Initialize();
    }

    void Start()
    {
        _gridInitializer.Initialize(_gridConfigs, _borderConfigs, _gridSize);
        _gridCells = _gridInitializer.CreateAndInitializeGrid(_verticalDirectionType, _horizontalDirectionType);
    }
    
    private void OnEnable()
    {
        Subscribe();
    }
    private void OnDisable()
    {
        UnSubscribe();
    }

    public void SetLevelItemConfig(LevelItemConfigs levelItemConfigs)
    {
        _levelItemConfigs = levelItemConfigs;
    }

    private void Initialize()
    {
        _mobileInputController = MobileInputController.Instance;
        _gridController = GridController.Instance;

        _gameCamera = CameraManager.Instance.GetCameraItem(ECameraType.GAME).Camera;
    }

    private void DetectClickedCell(Vector2 inputPosition)
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

    private void MoveCellsIfNeeded()
    {
        List<GridCell> filledCells = GetFilledCells();

        MoveVertical(delegate { MoveHorizontal(); });
    }

    private void MoveHorizontal(Action callback = null)
    {
        List<GridCell> filledCells = GetFilledCells();

        int movedCellsCount = 0;

        var movedCells = GetHorizontalMoveCells(filledCells);
        _gridInitializer.SetCellsNeighbors(_gridCells);

        movedCellsCount = movedCells.Count;

        if(movedCellsCount == 0)
        {
            callback?.Invoke();
            return;
        }

        foreach (var filledCell in movedCells)
        {
            filledCell.onCompleteMove += CheckForAllCellsMoved;
            filledCell.MoveByDirection(EDirectionMoveType.HORIZONTAL);
        }

        void CheckForAllCellsMoved()
        {
            movedCellsCount--;

            if(movedCellsCount == 0)
            {
                callback?.Invoke();
            }
        }
    }

    private void MoveVertical(Action callback)
    {
        List<GridCell> filledCells = GetFilledCells();

        int movedCellsCount = 0;

        var movedCells = GetVerticalMoveCells(filledCells);
        _gridInitializer.SetCellsNeighbors(_gridCells);

        movedCellsCount = movedCells.Count;

        if(movedCellsCount == 0)
        {
            callback?.Invoke();
            return;
        }

        foreach (var filledCell in movedCells)
        {
            filledCell.onCompleteMove += CheckForAllCellsMoved;
            filledCell.MoveByDirection(EDirectionMoveType.VERTICAL);
        }

        void CheckForAllCellsMoved()
        {
            movedCellsCount--;

            if(movedCellsCount == 0)
            {
                callback?.Invoke();
            }
        }
    }

    private List<CellController> GetVerticalMoveCells(List<GridCell> filledCells)
    {
        List<CellController> movedCells = new List<CellController>();

        foreach (var filledCell in filledCells)
        {
            var targetGridCell = _gridCellsController.TryGetTargetGridCell(filledCell, EDirectionType.BOTTOM);

            if(targetGridCell != null)
            {
                filledCell.CellController.AddElementToMovePath(targetGridCell);

                movedCells.Add(filledCell.CellController);

                UpdateCellsInfo(filledCell, targetGridCell);
            }
        }

        return movedCells;
    }

    private EDirectionType GetVerticalDirectionType(EVerticalDirectionType verticalDirectionType)
    {
        switch (verticalDirectionType)
        {
            case EVerticalDirectionType.DOWN: return EDirectionType.BOTTOM;
            case EVerticalDirectionType.TOP: return EDirectionType.TOP;
        }

        return EDirectionType.NONE;
    }

    private List<CellController> GetHorizontalMoveCells(List<GridCell> filledCells)
    {
        List<CellController> movedCells = new List<CellController>();

        int emptyElementsCount = 0;
        
        for (int i = 0; i < _gridSize.Width; i++)
        {
            if(_gridCells[i, 0].CellType == ECellType.EMPTY)
            {
                emptyElementsCount++;
            }
            else if(emptyElementsCount > 0)
            {
                for (int j = 0; j < _gridSize.Height; j++)
                {
                    var currentCell = _gridCells[i, j];
                    var targetCell = _gridCells[i - emptyElementsCount, j];

                    if(currentCell.CellController == null) continue;

                    currentCell.CellController.AddElementToMovePath(targetCell);

                    movedCells.Add(currentCell.CellController);

                    UpdateCellsInfo(currentCell, targetCell);
                }
            }
        }

        return movedCells;
    }

    private void UpdateCellsInfo(GridCell movedCell, GridCell targetCell)
    {
        targetCell.CellController = movedCell.CellController;
        targetCell.CellType = movedCell.CellType;

        movedCell.CellController = null;
        movedCell.CellType = ECellType.EMPTY;
    }

    public GridCell GetGridCellByPosition(Vector2 worldPosition)
    {
        for(int i = 0; i < _gridSize.Width; i++)
        {
            for(int j = 0; j < _gridSize.Height; j++)
            {
                if(Vector3.Distance(_gridCells[i, j].GlobalPosition, worldPosition) < _clickDistance)
                {
                    return _gridCells[i, j];
                }
            }
        }

        return null;
    }

    private List<GridCell> GetFilledCells()
    {
        List<GridCell> filledCells = new List<GridCell>();

        for (int i = 0; i < _gridSize.Width; i++)
        {
            for (int j = 0; j < _gridSize.Height; j++)
            {
                GridCell gridCell = _gridCells[i, j];
                 
                if(gridCell.CellController == null || gridCell.CellType != ECellType.FILL) continue;

                filledCells.Add(gridCell);
            }
        }

        return filledCells;
    }

    private List<int> GetEmptyRawIndexes()
    {
        List<int> emptyRawIndexes = new List<int>();

        for (int i = 0; i < _gridSize.Width; i++)
        {
            if(IsEmptyRaw(i))
            {
                emptyRawIndexes.Add(i);
            }
        }

        return emptyRawIndexes;
    }

    private bool IsEmptyRaw(int rawIndex)
    {
        for(int i = 0; i < _gridSize.Height; i++)
        {
            if(_gridCells[rawIndex, i].CellType == ECellType.FILL)
            {
                return false;
            }
        }

        return true;
    }

    private void OnLevelCompleted(ELevelCompleteReason levelCompleteReason)
    {
        onLevelComplete?.Invoke(levelCompleteReason);
    }

    private void SetupCorners(Vector2[,] positions)
    {
        _border.SetupLineRenderer(positions, _borderConfigs);
    }
    
    private void Subscribe()
    {
        _mobileInputController.onPointerDown += DetectClickedCell;
        _gridCellsController.onDisableCells += MoveCellsIfNeeded;
        _gridInitializer.onSetCorners += SetupCorners;
    }
    private void UnSubscribe()
    {
        _mobileInputController.onPointerDown -= DetectClickedCell;
        _gridCellsController.onDisableCells -= MoveCellsIfNeeded;
        _gridInitializer.onSetCorners -= SetupCorners;
    }
}
