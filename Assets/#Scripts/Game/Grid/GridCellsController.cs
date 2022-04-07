using System;
using System.Collections.Generic;
using UnityEngine;

public class GridCellsController : MonoBehaviour
{
    public void OnGridsCellInitialize(CellController cellController)
    {

    }

    public void OnClickCell(GridCell gridCell)
    {
        FindSameColorClosestCells(gridCell);
    }

    public void FindSameColorClosestCells(GridCell gridCell)
    {
        ECellsColor clickedCellColor = gridCell.CellController.CellsColorGroup.CellsColor;
        List<GridCell> closestGridCells = new List<GridCell>();

        closestGridCells.Add(gridCell);

        GetSameColorNeighbors(gridCell, closestGridCells);

        DisableCells(closestGridCells);

        void GetSameColorNeighbors(GridCell checkedGridCell, List<GridCell> closestGridCells)
        {
            if(checkedGridCell.CellType == ECellType.FILL)
            {
                GetNeighborsCells(checkedGridCell, closestGridCells);
            }
        }

        void GetNeighborsCells(GridCell checkedGridCell, List<GridCell> closestGridCells)
        {
            int gridCellsCount = closestGridCells.Count;

            foreach(var closestCell in checkedGridCell.ElementsInDirection)
            {
                GridCell neighborGridCell = closestCell.Value;

                if(closestGridCells.Contains(neighborGridCell)) continue;

                if (neighborGridCell.CellController.CellsColorGroup.CellsColor == clickedCellColor)
                {
                    closestGridCells.Add(neighborGridCell);
                }
            }

            if (closestGridCells.Count == 1) return;

            if (gridCellsCount != closestGridCells.Count)
            {
                for(int i = gridCellsCount - 1; i < closestGridCells.Count; i++)
                {
                    GetSameColorNeighbors(closestGridCells[i], closestGridCells);
                }

            }
        }
    }

    private void DisableCells(List<GridCell> closestCells)
    {
        foreach(var cell in closestCells)
        {
            Debug.Log(cell.CellController.CellsColorGroup.CellsColor);

            cell.CellType = ECellType.EMPTY;
            cell.CellController.ResetColor();
        }
    }
    
    private GridCell GetUnderCellFreeCell(GridCell targetCell, Action callback)
    {
        CellsLocalPosition cellsLocalPosition = targetCell.CellsLocalPosition;

        GridCell bottomCell = targetCell.ElementsInDirection[EDirectionType.BOTTOM];

        if (bottomCell.CellType == ECellType.EMPTY)
        {
            //GetUnderCellFreeCell(bottomCell, callback);
        }
        else
        {
            targetCell.MoveToGridCellByPath();
        }

        return bottomCell;
    }
}
