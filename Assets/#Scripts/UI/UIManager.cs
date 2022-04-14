using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private List<BaseScreen> _screens = new List<BaseScreen>();

    private IScreen _currentScreen = null;

    public void ShowScreen(EScreenType screenType)
    {
        HideCurrentScreen();

        _currentScreen = GetScreen(screenType);

        _currentScreen.DoShow();
    }

    public void DisableScreens()
    {
        foreach(IScreen screen in _screens)
        {
            screen.DoHide(true);
        }
    }

    private void HideCurrentScreen()
    {
        if(_currentScreen != null)
        {
            _currentScreen.DoHide();
        } 
    }

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
