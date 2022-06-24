using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleteController
{
    public event Action<ELevelCompleteReason> onLevelCompleted = default;
    
    private readonly ClosestCellsController _closestCellsController = new ClosestCellsController();
    
    public void CheckLevelState(List<GridCell> filledCells)
    {
        if(!IsAnyMoves(filledCells))
        {
            onLevelCompleted?.Invoke(ELevelCompleteReason.WIN);
        }
    }

    private bool IsAnyMoves(List<GridCell> filledCells)
    {
        if (filledCells.Count == 0) return false;

        foreach (var filledCell in filledCells)
        {
            if (_closestCellsController.GetCellNeighbourCount(filledCell) > 0) return true;
        }

        return false;
    }
}