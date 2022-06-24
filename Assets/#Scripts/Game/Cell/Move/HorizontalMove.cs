using System.Collections.Generic;

public class HorizontalMove : BaseMove
{
    protected override EDirectionMoveType GetDirectionMoveType() => EDirectionMoveType.HORIZONTAL;
    protected override (int, int) GetGridData() => (Rows, Columns);

    public HorizontalMove(GridCell[,] gridCells) : base(gridCells) { }

    protected override void GetMovedElements(int externalId, int internalId, ref List<MovableElement> movableElements)
    {
        var currentCell = GridCells[externalId + 1, internalId];
        var targetCell = GridCells[externalId, internalId];

        if (currentCell.CellState == ECellState.EMPTY || targetCell.CellState == ECellState.FILL) return;

        for (int i = externalId; i >= 0; i--)
        {
            if (GridCells[i, internalId].CellState == ECellState.EMPTY)
            {
                targetCell = GridCells[i, internalId];
            }
            else break;
        }
        
        MovableElement movableElement = (MovableElement) currentCell.Element;

        movableElement.SetTarget(targetCell);
        movableElements.Add(movableElement);

        UpdateCellsInfo(currentCell, targetCell);
    }
}
