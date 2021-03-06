using System;
using UnityEngine;

public abstract class BaseElement : MonoBehaviour, IElement
{
    public event Action<IElement> onDestroyElement;
    
    [SerializeField] protected Transform _elementTransform = null;

    protected abstract IElementConfigs ElementConfigs { get; }
    protected abstract void InIt();
    public abstract EElementType ElementType { get; }
    
    public void DestroyCell()
    {
        Destroy(gameObject);
        
        onDestroyElement?.Invoke(this);
    }

    public void InIt(Vector2 worldPosition, Vector2 cellSize)
    {
        SetupTransform(worldPosition,cellSize);

        InIt();
    }

    private void SetupTransform(Vector2 worldPosition, Vector2 cellSize)
    {
        _elementTransform.position = worldPosition;
        
        _elementTransform.localScale = cellSize;
    }
}