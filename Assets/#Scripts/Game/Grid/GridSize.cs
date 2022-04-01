using System;
using UnityEngine;

[Serializable]
public struct GridSize
{
    [SerializeField] private int _width;
    [SerializeField] private int _height;

    public int Width => _width;
    public int Height => _height;
}
