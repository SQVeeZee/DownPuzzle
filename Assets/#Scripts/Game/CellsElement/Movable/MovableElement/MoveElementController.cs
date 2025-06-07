using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class MoveElementController : MonoBehaviour
{
    private GridCell _targetGridCell = null;

    private Transform _elementTransform = null;
    private CancellationTokenSource _moveCancellationTokenSource = null;
    
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

        _moveCancellationTokenSource = new CancellationTokenSource();

        MoveAsync(_moveCancellationTokenSource.Token).Forget();

        async UniTaskVoid MoveAsync(CancellationToken token)
        {
            await DoMove(targetMovePosition, moveElementModel.MoveSpeed, token);
            OnFinishMove();
        }

        void OnFinishMove()
        {
            ResetTargetCell();

            callback?.Invoke();
        }
    }

    private async UniTask DoMove(Vector2 targetPosition, float speedMultiply, CancellationToken token)
    {
        float time = 0f;

        while (time < 1f)
        {
            token.ThrowIfCancellationRequested();

            time += Time.deltaTime * speedMultiply;

            _elementTransform.position = Vector2.MoveTowards(_elementTransform.position, targetPosition, time);

            await UniTask.Yield();
        }

        _elementTransform.position = targetPosition;
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
        if(_moveCancellationTokenSource == null) return;

        _moveCancellationTokenSource.Cancel();
        _moveCancellationTokenSource.Dispose();
        _moveCancellationTokenSource = null;
    }

    private void ResetTargetCell() => _targetGridCell = null;
}
