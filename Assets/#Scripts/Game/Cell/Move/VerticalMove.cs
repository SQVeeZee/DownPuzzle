using System.Collections.Generic;

public class VerticalMove : BaseMove
{
    protected override EDirectionMoveType GetDirectionMoveType() => EDirectionMoveType.VERTICAL;
    protected override (int, int) GetGridData() => (Columns, Rows);

    public VerticalMove(GridCell[,] gridCells) : base(gridCells) { }

    protected override void GetMovedElements(int externalId, int internalId, ref List<MovableElement> movableElements)
    {
        var currentCell = GridCells[internalId, externalId + 1];
        var targetCell = GridCells[internalId, externalId];

        if (currentCell.CellState == ECellState.EMPTY || targetCell.CellState == ECellState.FILL) return;

        for (int i = externalId; i >= 0; i--)
        {
            if (GridCells[internalId, i].CellState == ECellState.EMPTY)
            {
                targetCell = GridCells[internalId, i];
            }
            else break;
        }
        
        MovableElement movableElement = (MovableElement) currentCell.Element;

        movableElement.SetTarget(targetCell);
        movableElements.Add(movableElement);

        UpdateCellsInfo(currentCell, targetCell);
    }
}
