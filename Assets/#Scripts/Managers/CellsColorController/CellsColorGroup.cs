using UnityEngine;
using System;

[Serializable]
public class CellsColorGroup
{
    [SerializeField] private ECellsColor _cellsColor = default;
    [SerializeField] private Color _color = default;
    
    public ECellsColor CellsColor => _cellsColor;
    public Color Color => _color;
}
