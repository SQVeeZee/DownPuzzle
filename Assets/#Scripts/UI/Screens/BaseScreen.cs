using System;
using DG.Tweening;
using UnityEngine;

public abstract class BaseScreen : MonoBehaviour, IScreen
{
    [Header("Screen")]
    [SerializeField] private EScreenType _screenType = EScreenType.NONE;
    [SerializeField] protected CanvasGroup _canvasGroup = null;
    [SerializeField] protected GameObject _canvas = null;

    private GeneralUIConfigs _generalUiConfigs = null; 

    private Tweener _tweener = null;

    public EScreenType ScreenType => _screenType;
    
    public void Initialize(GeneralUIConfigs generalUiConfigs)
    {
        _generalUiConfigs = generalUiConfigs;
    }
    
    public void DoHide(bool force = false, Action<IScreen> callback = null)
    {
        ResetTween();
        
        if(force)
        {
           HideScreen();
        }
        else
        {
            _tweener = _canvasGroup.DOFade(0, _generalUiConfigs.HideTime).OnComplete(HideScreen);
        }

        void HideScreen()
        {
            _canvasGroup.alpha = 0;
            _canvas.SetActive(false);
        
            callback?.Invoke(this);
        }
    }
    public void DoShow(bool force = false, Action<IScreen> callback = null)
    {
        ResetTween();

        _canvasGroup.alpha = 0;
        _canvas.SetActive(true);

        if(force)
        {
           ShowScreen();
        }
        else
        {
            _tweener = _canvasGroup.DOFade(1, _generalUiConfigs.ShowTime).OnComplete(ShowScreen);
        }

        void ShowScreen()
        {
            _canvasGroup.alpha = 1;

            callback?.Invoke(this);
        }
    }
    
    private void ResetTween()
    {
        _tweener?.Kill();
        _tweener = null;
    }
}
