using System;
using UnityEngine;

public class MoveElementPresenter : MonoBehaviour
{
    [SerializeField] private MoveElementController _moveElementController = null;
    
    private MovableElementConfigs _movableElementConfigs = null;
    private MoveElementModel _moveElementModel = null;
    private int ABCD = 0;
    public void Initialize(Transform elementTransform, IElementConfigs elementConfigs)
    {
        _movableElementConfigs = (MovableElementConfigs)elementConfigs;
        
        _moveElementModel = new MoveElementModel(_movableElementConfigs.MoveSpeed);

        _moveElementController.Initialize(elementTransform);
    }

    public void SetMoveTarget(GridCell gridCell) => _moveElementController.SetMoveTarget(gridCell);
    public void MoveToTarget(EDirectionMoveType directionMoveType, Action callback) => 
        _moveElementController.MoveToTarget(_moveElementModel, directionMoveType, callback);
}
