using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellsColorController : Singleton<CellsColorController>
{
    [SerializeField] private List<CellsColorGroup> _cellsColorGroup = new List<CellsColorGroup>();

    public CellsColorGroup GetRandomColorGroup()
    {
        return _cellsColorGroup[UnityEngine.Random.Range(0, _cellsColorGroup.Count)];
    }
}


