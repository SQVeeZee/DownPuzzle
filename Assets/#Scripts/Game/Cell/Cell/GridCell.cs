using System.Collections.Generic;

public class GridCell
{
    public IElement Element { get; set; }

    public bool IsSelected { get; set; } = false;
    public ECellState CellState => Element != null ? ECellState.FILL : ECellState.EMPTY;
    public CellPosition CellPosition { get; set; }
    public Dictionary<EDirectionType, GridCell> ElementsInDirection { get; set; }

}
