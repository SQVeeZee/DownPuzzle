using System;
using System.Collections.Generic;
using UnityEngine;

public class GridCellsController : MonoBehaviour
{
    public event Action onDisableCells = null;

    private List<GridCell> _closestGridCells = new List<GridCell>();

    public void OnGridsCellInitialize(CellController cellController)
    {

    }

    public void OnClickCell(GridCell gridCell)
    {
        if(_closestGridCells.Count > 0)
        {
            if(_closestGridCells.Contains(gridCell))
            {
                 if(_closestGridCells.Count > 1)
                    DisableCells(_closestGridCells);
            }
            else
            {
                UnSelectCells();

                FindSameColorClosestCells(gridCell);
            }
        }
        else
        {
            FindSameColorClosestCells(gridCell);
        }
    }

    public void FindSameColorClosestCells(GridCell gridCell)
    {
        ECellsColor clickedCellColor = gridCell.CellController.CellsColorGroup.CellsColor;

        _closestGridCells.Add(gridCell);

        GetSameColorNeighbors(gridCell, _closestGridCells);

        SetClosestCellsSelectState(true);

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

                if(closestGridCells.Contains(neighborGridCell) || neighborGridCell.CellController == null) continue;

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

    public GridCell TryGetTargetGridCell(GridCell movedCell, EDirectionType verticalDirectionType)
    {
        GridCell gridCellsPath = null;

        if(!movedCell.ElementsInDirection.ContainsKey(verticalDirectionType)) return gridCellsPath;

        gridCellsPath = GetVerticalTarget(movedCell, verticalDirectionType);

        return gridCellsPath;
    }

    private void UnSelectCells()
    {
        SetClosestCellsSelectState(false);
        _closestGridCells.Clear();
    }

    private void DisableCells(List<GridCell> closestCells)
    {
        foreach(var cell in closestCells)
        {
            cell.CellController.DestroyCell();
            cell.CellController = null;
            cell.CellType = ECellType.EMPTY;
        }

        closestCells.Clear();

        onDisableCells?.Invoke();
    }

    private void SetClosestCellsSelectState(bool state)
    {
        foreach (var gridCell in _closestGridCells)
        {
            gridCell.CellController.SetSelectCellState(state);
        }
    }

    private GridCell GetVerticalTarget(GridCell targetCell, EDirectionType verticalDirectionType)
    {
        List<GridCell> verticalMovePath = new List<GridCell>(); 

        GridCell neighborCellByDirection = targetCell.ElementsInDirection[verticalDirectionType];

        if(neighborCellByDirection.CellType != ECellType.EMPTY) return null;
        
        verticalMovePath.Add(neighborCellByDirection);

        while(neighborCellByDirection.CellType == ECellType.EMPTY)
        {
            if(!neighborCellByDirection.ElementsInDirection.ContainsKey(verticalDirectionType)) return verticalMovePath[verticalMovePath.Count - 1];

            neighborCellByDirection = neighborCellByDirection.ElementsInDirection[verticalDirectionType];
            
            if(neighborCellByDirection.CellType != ECellType.EMPTY) return verticalMovePath[verticalMovePath.Count - 1];
            
            verticalMovePath.Add(neighborCellByDirection);
        }

        return verticalMovePath[verticalMovePath.Count - 1];
    }
}
