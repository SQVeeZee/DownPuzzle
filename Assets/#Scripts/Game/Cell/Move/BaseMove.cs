using System;
using System.Collections.Generic;

public abstract class BaseMove : IMove
{
    protected readonly GridCell[,] GridCells = null;
    protected abstract EDirectionMoveType GetDirectionMoveType();
    protected abstract (int, int) GetGridData();
    protected abstract void GetMovedElements(int externalId, int internalId, ref List<MovableElement> movableElements);

    protected readonly int Rows = 0;
    protected readonly int Columns = 0;

    public Action MoveCallback { get; set; }
    
    protected BaseMove(GridCell[,] gridCells)
    {
        GridCells = gridCells;
        
        Rows = GridCells.GetLength(0);
        Columns = GridCells.GetLength(1);
    }
    
    protected void UpdateCellsInfo(GridCell movedCell, GridCell targetCell)
    {
        targetCell.Element = movedCell.Element;
        movedCell.Element = null;
    }

    public void Move()
    {
        var movableElements = GetMovableElementsElements();

        int elementsCount = movableElements.Count;

        if(elementsCount == 0)
        {
            MoveCallback?.Invoke();
            return;
        }

        foreach (var movableElement in movableElements)
        {
            movableElement.Move(GetDirectionMoveType(), CheckForAllCellsMoved);
        }

        void CheckForAllCellsMoved()
        {
            elementsCount--;

            if(elementsCount == 0)
            {
                MoveCallback?.Invoke();
            }
        }
    }

    private List<MovableElement> GetMovableElementsElements()
    {
        List<MovableElement> movableElements = new List<MovableElement>();

        (int externalCycleData, int internalCycleData) = GetGridData();

        for (int i = 0; i < externalCycleData - 1; i++)
        {
            for (int j = 0; j < internalCycleData; j++)
            {
                GetMovedElements(i,j, ref movableElements);
            }
        }

        return movableElements;
    }
}
