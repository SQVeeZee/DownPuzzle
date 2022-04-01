using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameGridController : MonoBehaviour
{
    [Header("Grid")]
    [SerializeField] private GridSize _gridSize = default;
    [SerializeField] private Vector2 _gridPosition = default;
    [SerializeField] private Vector2 _gridScreenSize = default;

    [Header("Prefabs")]
    [SerializeField] private CellController _cellController = null;

    private float _cellSize = 1;

    private float _clickDistance => _cellSize / 2;

    private List<GridCell> _gridsCells = default;
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

    public void CreateFillGrid()
    {
        _gridsCells = new List<GridCell>();

        Vector2 firstGlobalCellPosition = GetStartCellsGlobalPosition();

        for (int i = 0; i < _gridSize.Height; i++)
        {
            for (int j = 0; j < _gridSize.Width; j++)
            {
                Vector2 gridsCellPosition = new Vector2(j, i);

                var globalCellPosition = firstGlobalCellPosition + gridsCellPosition;

                GridCell gridCell = GetNewFilledGridCell(globalCellPosition, gridsCellPosition);

                var cellController = InstantiateCell(gridCell);
                cellController.SetCellColor(_cellsColorController.GetRandomColorGroup());

                gridCell.CellController = cellController;

                _gridsCells.Add(gridCell);
            }
        }
    }

    private CellController InstantiateCell(GridCell gridCell)
    {
        CellController cellController = Instantiate(_cellController, transform);

        cellController.CellTransform.position = gridCell.GlobalPosition;

        return cellController;
    }

    private Vector2 GetStartCellsGlobalPosition() => _gridPosition - new Vector2(_gridSize.Width - 1, _gridSize.Height - 1) / 2;

    private GridCell GetNewFilledGridCell(Vector2 globalPosition, Vector2 gridPosition)
    {
        GridCell newGridCell = new GridCell();
        
        newGridCell.CellType = ECellType.FILL;
        newGridCell.GlobalPosition = globalPosition;
        newGridCell.GridPosition = gridPosition;

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
        clickedGridCell.CellController.ResetColor();
    }

    private GridCell GetGridCellByPosition(Vector2 worldPosition)
    {
        foreach(var gridCell in _gridsCells)
        {
            if(Vector3.Distance(gridCell.GlobalPosition, worldPosition) < _clickDistance)
            {
                return gridCell;
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