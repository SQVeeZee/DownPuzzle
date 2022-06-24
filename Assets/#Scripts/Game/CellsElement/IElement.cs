using UnityEngine;

public interface IElement
{
    void InIt(Vector2 worldPosition, Vector2 cellSize);
    EElementType ElementType { get; }
}
