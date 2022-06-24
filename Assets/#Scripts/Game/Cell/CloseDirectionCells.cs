using System;
using System.Collections.Generic;

public class CloseDirectionCells
{
    private int _gridWidth = 0;
    private int _gridHeight = 0;

    private EDirectionType[] _directionTypes;
    
    public void SetCellsNeighbors(GridCell[,] gridCells)
    {
        _directionTypes = (EDirectionType[]) Enum.GetValues(typeof(EDirectionType));

        _gridWidth = gridCells.GetLength(0);
        _gridHeight = gridCells.GetLength(1);
        
        for (int i = 0; i < _gridWidth; i++)
        {
            for (int j = 0; j < _gridHeight; j++)
            {
                SetCellsNeighbors(gridCells, gridCells[i, j]);
            }
        }
    }
    
    private void SetCellsNeighbors(GridCell[,] gridCells, GridCell gridCell)
    {
        gridCell.ElementsInDirection = new Dictionary<EDirectionType, GridCell>(4);

        foreach (EDirectionType directionType in _directionTypes)
        {
            Position cellsLocalPosition =
                Direction.GetGridLocalPositionByDirectionType(gridCell.CellPosition, directionType);

            var isCell = TryGetCellByLocalPosition(gridCells, cellsLocalPosition, out GridCell targetGridCell);

            if (isCell)
            {
                gridCell.ElementsInDirection.Add(directionType, targetGridCell);
            }
        }
    }

    private bool TryGetCellByLocalPosition(GridCell[,] gridCells, Position cellsPositions, out GridCell gridCell)
    {
        if(IsGridCellExists(cellsPositions))
        {
            gridCell = gridCells[cellsPositions.PositionX, cellsPositions.PositionY];
            return true;
        }

        gridCell = null;
        return false;
    }

    private bool IsGridCellExists(Position cellsPositions) =>
        0 <= cellsPositions.PositionX && cellsPositions.PositionX < _gridWidth &&
        0 <= cellsPositions.PositionY && cellsPositions.PositionY < _gridHeight;
}
