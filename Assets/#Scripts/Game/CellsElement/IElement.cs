using System;
using UnityEngine;

public interface IElement
{
    event Action<IElement> onDestroyElement;
    void InIt(Vector2 worldPosition, Vector2 cellSize);
    EElementType ElementType { get; }
    void DestroyCell();
}
