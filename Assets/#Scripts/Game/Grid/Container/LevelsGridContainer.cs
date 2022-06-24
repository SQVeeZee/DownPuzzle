using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelsGridContainer : MonoBehaviour
{
    [SerializeField] private GridInitializer _gridInitializer = null;

    public GridCell[,] GetGrid() => _gridCells.Length > 0 ? _gridCells : throw new Exception("Can't get empty grid");

    private GridCell[,] _gridCells = null;

    private int _width = 0;
    private int _height = 0;
    
    private void OnEnable()
    {
        Subscribe();
    }

    private void OnDisable()
    {
        UnSubscribe();
    }

    public List<GridCell> GetFilledCells()
    {
        List<GridCell> filledCells = new List<GridCell>();

        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                GridCell gridCell = _gridCells[i, j];
                 
                if(gridCell.CellState != ECellState.FILL) continue;

                filledCells.Add(gridCell);
            }
        }

        return filledCells;
    }
    
    public bool TryGetCellByGlobalPosition(Vector2 worldPosition, float distance, out GridCell gridCell)
    {
        gridCell = null;
        
        for(int i = 0; i < _width; i++)
        {
            for(int j = 0; j < _height; j++)
            {
                if(Vector2.Distance(_gridCells[i, j].CellPosition.GlobalPosition, worldPosition) <= distance)
                {
                    gridCell = _gridCells[i, j];
                    return true;
                }
            }
        }

        return false;
    }
    
    private void OnGridCreated(GridCell[,] gridCells)
    {
        _gridCells = gridCells;
        
        _width = _gridCells.GetUpperBound(0) + 1;
        _height = _gridCells.Length / _width;
    }

    private void Subscribe()
    {
        _gridInitializer.onGridCreated += OnGridCreated;
    }

    private void UnSubscribe()
    {
        _gridInitializer.onGridCreated -= OnGridCreated;
    }
}
