using System.Collections.Generic;
using UnityEngine;

public class GridCell
{
    public ECellType CellType { get; set; }
    public Vector2 GlobalPosition { get; set; }
    public CellsLocalPosition CellsLocalPosition { get; set; }
    public CellController CellController { get; set; }
    public Dictionary<EDirectionType, GridCell> ElementsInDirection { get; set; } = new Dictionary<EDirectionType, GridCell>();

    public void MoveToGridCellByPath()
    {

    }
}
