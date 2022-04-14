using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CellController : MonoBehaviour
{
    public event Action onCompleteMove;

    [SerializeField] private Transform _cellTransform = null;
    [SerializeField] private SpriteRenderer _spriteRenderer = null;
    [SerializeField] private GameObject _selectCellIdentifier = null;

    [Header("Settings")]
    [SerializeField] private float _moveSpeed = 2;

    public Transform CellTransform => _cellTransform;
    public CellsColorGroup CellsColorGroup => _cellsColorGroup;
    private CellsColorGroup _cellsColorGroup = null;
    private GridCell _targetGridCell = null;

    public void SetCellColor(CellsColorGroup cellsColorGroup)
    {
        _cellsColorGroup = cellsColorGroup;

        SetSelectCellState(false);
    }

    public void DestroyCell()
    {
        _spriteRenderer.color = Color.white;

        Destroy(gameObject);

        //_selectCellIdentifier.SetActive(false);
    }

    public void SetSelectCellState(bool state)
    {
        if(_cellsColorGroup != null)
        {
            if(!state)
            {
                _spriteRenderer.color = _cellsColorGroup.DefaultColor;
            }
            else
            {
                _spriteRenderer.color = _cellsColorGroup.ActiveColor;
            }
        }

        _selectCellIdentifier.SetActive(state);
    }

    public void AddElementToMovePath(GridCell targetCell)
    {
        _targetGridCell = targetCell;
    }

    public void MoveToGridCell(EDirectionMoveType directionMoveType)
    {
        if(_targetGridCell == null) 
        {
            OnFinishMove();
            return;
        }

        Vector2 targetMovePosition = new Vector2(0, 0);

        targetMovePosition = GetTargetMovePosition(directionMoveType, targetMovePosition);

        Vector2 currentPosition = (Vector2)_cellTransform.position;

        float length = (targetMovePosition - currentPosition).magnitude;
        float time = length / _moveSpeed;

        _cellTransform.DOMove(targetMovePosition, time).SetEase(Ease.Linear).OnComplete(OnFinishMove);

        void OnFinishMove()
        {
            onCompleteMove?.Invoke();
        }
    }

    private Vector2 GetTargetMovePosition(EDirectionMoveType directionMoveType, Vector2 targetMovePosition)
    {
        Vector2 targetPosition = _targetGridCell.GlobalPosition;
        Vector2 currentPosition = (Vector2)_cellTransform.position;

        switch (directionMoveType)
        {
            case EDirectionMoveType.VERTICAL:
            if(targetPosition.y != currentPosition.y)
            {
                targetMovePosition.x = currentPosition.x;
                targetMovePosition.y = targetPosition.y;
            }
            break;
            
            case EDirectionMoveType.HORIZONTAL:
            if(targetPosition.x != currentPosition.x)
            {
                targetMovePosition.y = currentPosition.y;
                targetMovePosition.x = targetPosition.x;
            }

            ResetTargetCell();

            break;
        }

        return targetMovePosition;
    }

    public void MoveByDirection(EDirectionMoveType directionMoveType)
    {
        MoveToGridCell(directionMoveType);   
    }

    public void ResetTargetCell()
    {
        _targetGridCell = null;
    }

    private Vector3[] ConvertVector2PathToVector3(List<Vector2> movePath)
    {
        Vector3[] convertedPath = new Vector3[movePath.Count];

        for (int i = 0; i < convertedPath.Length; i++)
        {
            convertedPath[i] = new Vector3(movePath[i].x, movePath[i].y, 0);
        }

        return convertedPath;
    }
}
public enum EDirectionMoveType
{
    NONE = 0,

    HORIZONTAL = 1,
    VERTICAL = 2,
}
