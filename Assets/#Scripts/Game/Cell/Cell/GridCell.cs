using System.Collections.Generic;

public class GridCell
{
    public GridCell(CellPosition cellPosition)
    {
        CellPosition = cellPosition;
        IsSelected = false;
    }
    
    public IElement Element { get; private set; }
    public bool IsSelected { get; set; }
    public ECellState CellState { get; private set; }
    public CellPosition CellPosition { get; }
    public Dictionary<EDirectionType, GridCell> ElementsInDirection { get; set; }

    public void SetElement(IElement item)
    {
        Element = item;

        if (item != null)
        {
            Element = item;

            CellState = ECellState.FILL;
            item.onDestroyElement += RemoveElement;
        }
    }
    
    public void RemoveElement() => RemoveElement(Element);

    private void RemoveElement(IElement item)
    {
        item.onDestroyElement -= RemoveElement;
        Element = null;

        CellState = ECellState.EMPTY;
    }
}
