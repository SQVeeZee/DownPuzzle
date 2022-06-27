using System;
using System.Collections.Generic;
using UnityEngine;

public class ElementsFactory : MonoBehaviour
{
    [SerializeField] private Transform _rootTransform = null;
    
    [Header("Elements")]
    [SerializeField] private FixedElement _fixedElement = null;
    [SerializeField] private MovableElement _movableElement = null;

    private List<IElement> _elements = new List<IElement>();
    
    public IElement CreateElement(EElementType elementType, Vector2 position, Vector2 elementSize)
    {
        IElement item = elementType switch
        {
            EElementType.FIXED => CreateCell(_fixedElement),
            EElementType.MOVABLE => CreateCell(_movableElement),
            _ => throw new Exception("Undefined movableElement")
        };
        
        if (item != null)
        {
            item.InIt(position, elementSize);
            item.onDestroyElement += RemoveItem;
            _elements.Add(item);
        }
        
        return item;
    }
    
    public void ClearCells()
    {
        foreach (var item in _elements)
        {
            item.onDestroyElement -= RemoveItem;

            item.DestroyCell();
        }

        _elements.Clear();
    }
    
    private IElement CreateCell(BaseElement element) => Instantiate(element, _rootTransform);

    private void RemoveItem(IElement item)
    {
        item.onDestroyElement -= RemoveItem;

        if(_elements.Contains(item))
            _elements.Remove(item);
    }    
}