using System;
using System.Collections;
using UnityEngine;

public class MoveElementController : MonoBehaviour
{
    private GridCell _targetGridCell = null;

    private Transform _elementTransform = null;
    private Coroutine _moveCoroutine = null;
    
    public void Initialize(Transform elementTransform)
    {
        _elementTransform = elementTransform;
    }
    
    public void SetMoveTarget(GridCell targetCell) => _targetGridCell = targetCell;

    public void MoveToTarget(MoveElementModel moveElementModel, EDirectionMoveType directionMoveType, Action callback)
    {
        if (_targetGridCell == null)
        {
            OnFinishMove();
            return;
        }

        ResetMoveIfNeeded();

        Vector2 targetMovePosition = GetTargetMovePosition(directionMoveType);

        _moveCoroutine = StartCoroutine(DoMove(targetMovePosition, moveElementModel.MoveSpeed, OnFinishMove));

        void OnFinishMove()
        {
            ResetTargetCell();
            
            callback?.Invoke();
        }
    }

    private IEnumerator DoMove(Vector2 targetPosition, float speedMultiply, Action callback)
    {
        float time = 0;

        while (time < 1f)
        {
            time += Time.deltaTime * speedMultiply;
            
            _elementTransform.position = Vector2.MoveTowards(_elementTransform.position, targetPosition, time);
            
            yield return 0;
        }
        
        _elementTransform.position = targetPosition;
        
        callback?.Invoke();
    }
    
    private Vector2 GetTargetMovePosition(EDirectionMoveType directionMoveType)
    {
        Vector2 currentPosition = _elementTransform.position;
        Vector2 targetPosition = _targetGridCell.CellPosition.GlobalPosition;

        return directionMoveType switch
        {
            EDirectionMoveType.VERTICAL => new Vector2(currentPosition.x, targetPosition.y),
            EDirectionMoveType.HORIZONTAL => new Vector2(targetPosition.x, currentPosition.y),
            _ => currentPosition
        };
    }

    private void ResetMoveIfNeeded()
    {
        if(_moveCoroutine == null) return;
        
        StopCoroutine(_moveCoroutine);
        _moveCoroutine = null;
    }

    private void ResetTargetCell() => _targetGridCell = null;
}
