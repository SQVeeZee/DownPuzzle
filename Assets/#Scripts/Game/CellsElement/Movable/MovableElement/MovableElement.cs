using System;
using DG.Tweening;
using UnityEngine;

public class MovableElement : BaseElement
{
    [SerializeField] private MoveElementPresenter _movePresenter = null;
    [SerializeField] private ElementsStylePresenter _stylePresenter = null;

    [Header("Configs")]
    [SerializeField] private MovableElementConfigs movableElementConfigs = null;
    
    protected override IElementConfigs ElementConfigs => movableElementConfigs;
    
    public override EElementType ElementType => EElementType.MOVABLE;

    protected override void InIt()
    {
        _stylePresenter.Initialize();
        _movePresenter.Initialize(_elementTransform, ElementConfigs);
    }

    #region StylePresenter

    public ECellsColorType GetElementColorType => _stylePresenter.GetColorType;

    public void Select() => _stylePresenter.SetSelectionState(true);
    public void UnSelect() => _stylePresenter.SetSelectionState(false);
    
    #endregion

    #region MovePresenter

    public void SetTarget(GridCell targetCell) => _movePresenter.SetMoveTarget(targetCell);

    public void Move(EDirectionMoveType directionMoveType, Action callback) =>
        _movePresenter.MoveToTarget(directionMoveType, callback);

    #endregion
}