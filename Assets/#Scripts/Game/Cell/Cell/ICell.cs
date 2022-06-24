using UnityEngine;

public interface ICell
{
    void Initialize(GridCell gridCell);
    
    ECellState CellState { get; set; }
    Vector2 GlobalPosition { get; set; }
    CellPosition CellPosition { get; set; }
}
