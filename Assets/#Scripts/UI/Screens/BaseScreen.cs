using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public abstract class BaseScreen : MonoBehaviour, IScreen
{
    [SerializeField] private EScreenType _screenType = EScreenType.NONE;
    [SerializeField] protected CanvasGroup _canvasGroup = null;
    [SerializeField] protected GameObject _canvas = null;

    [Header("AnimationSettings")]
    [SerializeField] private float _showTime = 0.5f;
    [SerializeField] private float _hideTime = 0.5f;

    public EScreenType ScreenType => _screenType;

    public void DoHide(bool force = false, Action callback = null)
    {
        if(force)
        {
           HideScreen();
        }
        else
        {
            _canvasGroup.DOFade(0, _hideTime).OnComplete(HideScreen);
        }

        void HideScreen()
        {
            _canvasGroup.alpha = 0;
            _canvas.SetActive(false);
        
            callback?.Invoke();
        }
    }


    public void DoShow(bool force = false, Action callback = null)
    {
        _canvasGroup.alpha = 0;
        _canvas.SetActive(true);

        if(force)
        {
           ShowScreen();
        }
        else
        {
            _canvasGroup.DOFade(1, _showTime).OnComplete(ShowScreen);
        }

        void ShowScreen()
        {
            _canvasGroup.alpha = 1;
            
            callback?.Invoke();
        }
    }
}
