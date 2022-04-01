using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellController : MonoBehaviour
{
    [SerializeField] private Transform _cellTransform = null;
    [SerializeField] private SpriteRenderer _spriteRenderer = null;

    public Transform CellTransform => _cellTransform;

    private CellsColorGroup _cellsColorGroup = null;

    public void SetCellColor(CellsColorGroup cellsColorGroup)
    {
        _cellsColorGroup = cellsColorGroup;

        _spriteRenderer.color = _cellsColorGroup.Color;
    }

    public void ResetColor()
    {
        _spriteRenderer.color = Color.white;
    }
}

