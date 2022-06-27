using System;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private List<BaseScreen> _screens = new List<BaseScreen>();
    
    private IScreen _currentScreen = null;

    public void Initialize(GeneralUIConfigs generalUIConfigs)
    {
        foreach (var screen in _screens)
        {
            screen.Initialize(generalUIConfigs);
        }
    }
    
    public void ShowScreen(EScreenType screenType, bool force = false, Action<IScreen> callback = null)
    {
        HideCurrentScreen();

        _currentScreen = GetScreen(screenType);

        _currentScreen.DoShow(force, OnShow);

        void OnShow(IScreen screen)
        {
            callback?.Invoke(screen);
        }
    }

    public void DisableScreens()
    {
        foreach(IScreen screen in _screens)
        {
            screen.DoHide(true);
        }
    }

    private void HideCurrentScreen() => _currentScreen?.DoHide();
    
    private IScreen GetScreen(EScreenType screenType)
    {
        foreach(IScreen screen in _screens)
        {
            if(screen.ScreenType == screenType)
            {
                return screen;
            }
        }

        return null;
    }
}
