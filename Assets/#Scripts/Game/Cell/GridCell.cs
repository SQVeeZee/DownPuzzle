using UnityEngine;
using System;

[Serializable]
public class GridCell
{
    public ECellType CellType { get; set; }
    public Vector2 GlobalPosition { get; set; }
    public Vector2 GridPosition { get; set; }
    public CellController CellController { get; set; }
}
