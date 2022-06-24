using System;
using UnityEngine;

public class ElementsFactory : MonoBehaviour
{
    [SerializeField] private Transform _rootTransform = null;
    
    [Header("Elements")]
    [SerializeField] private FixedElement _fixedElement = null;
    [SerializeField] private MovableElement _movableElement = null;

    public IElement CreateElement(EElementType elementType, Vector2 position, Vector2 elementSize)
    {
        IElement element = elementType switch
        {
            EElementType.FIXED => CreateCell(_fixedElement),
            EElementType.MOVABLE => CreateCell(_movableElement),
            _ => throw new Exception("Undefined movableElement")
        };
        element?.InIt(position, elementSize);
        
        return element;
    }
    
    private IElement CreateCell(BaseElement element) => Instantiate(element, _rootTransform);
}