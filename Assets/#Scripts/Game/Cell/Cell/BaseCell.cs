using UnityEngine;

public abstract class BaseCell : MonoBehaviour, ICell
{
    public void Initialize(GridCell gridCell)
    {
        
    }

    public ECellState CellState { get; set; }
    public Vector2 GlobalPosition { get; set; }
    public CellPosition CellPosition { get; set; }
}
