using System;
using System.Collections.Generic;
using UnityEngine;

public class ClosestCellsController
{
    public event Action<Stack<GridCell>> onSelectElements = null;
    
    public void SetClickedCell(GridCell clickedCell)
    {
        Stack<GridCell> gridCells = new Stack<GridCell>();

        gridCells.Push(clickedCell);
        
        FindSameColorClosestCells(clickedCell, gridCells);

        onSelectElements?.Invoke(gridCells);
    }


    public int GetCellNeighbourCount(GridCell gridCell)
    {
        Stack<GridCell> gridCells = new Stack<GridCell>();

        FindSameColorClosestCells(gridCell, gridCells);
        
        return gridCells.Count;
    }
    
    private void FindSameColorClosestCells(GridCell gridCell, Stack<GridCell> gridCells)
    {
        GetNeighborsCells(gridCell, gridCells);

        void GetNeighborsCells(GridCell checkedGridCell,  Stack<GridCell> closestGridCells)
        {
            foreach (var closestCell in checkedGridCell.ElementsInDirection)
            {
                GridCell neighborGridCell = closestCell.Value;

                if (closestGridCells.Contains(neighborGridCell)) continue;

                if (neighborGridCell.CellState != ECellState.FILL) continue;

                MovableElement movableElement = (MovableElement)gridCell.Element;
                MovableElement checkedElement = (MovableElement)neighborGridCell.Element;
                
                if (movableElement.GetElementColorType != checkedElement.GetElementColorType) continue;

                closestGridCells.Push(neighborGridCell);
                
                GetSameColorNeighbors(neighborGridCell, closestGridCells);
            }
        }
        
        void GetSameColorNeighbors(GridCell checkedGridCell, Stack<GridCell> closestGridCells)
        {
            if(checkedGridCell.CellState == ECellState.FILL)
            {
                GetNeighborsCells(checkedGridCell, closestGridCells);
            }
        }
    }
}