public class CellsCreator
{
    public GridCell GetNewCell(IElement element, CellPosition cellPosition)
    {
        var gridCell = GetNewCell(cellPosition);
        gridCell.Element = element;
        
        return gridCell;
    }
    
    private GridCell GetNewCell(CellPosition cellPosition) => new GridCell
    {
        CellPosition = cellPosition,
    };
}
