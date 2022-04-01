using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MobileInputController : Singleton<MobileInputController>, IPointerDownHandler, IPointerUpHandler
{
    public event Action<Vector2> onPointerDown = null;
    public event Action<Vector2> onPointerUp = null;
    public event Action<Vector2> onPointerClick = null;

    private Vector2 _pointerDownPosition = default;

    public void OnPointerDown(PointerEventData eventData)
    {
        _pointerDownPosition = eventData.position;

        onPointerDown?.Invoke(_pointerDownPosition);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Vector2 pointerUpPosition = eventData.position;

        onPointerUp?.Invoke(pointerUpPosition);

        if (Vector2.Distance(pointerUpPosition, _pointerDownPosition) <= 1)
        {
            onPointerClick?.Invoke(pointerUpPosition);
        }
    }
}
