using UnityEngine;
using System;

[Serializable]
public class CellsColorGroup
{
    [SerializeField] private ECellsColor _cellsColor = default;
    [SerializeField] private Color _defaultColor = default;
    [SerializeField] private Color _activeColor = default;
    
    public ECellsColor CellsColor => _cellsColor;
    public Color DefaultColor => _defaultColor;
    public Color ActiveColor => _activeColor;
}
