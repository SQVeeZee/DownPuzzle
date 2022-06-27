using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIButtonHandler : MonoBehaviour, IUIElementsHandler
{
    public enum EUIButtonType
    {
        NONE = 0,
        
        WIN_CONTINUE = 10,
    }
    
    private Dictionary<EUIButtonType, UIButton> _buttonGroup;
    
    private void Awake()
    {
        RegisterButton();
    }

    public void Subscribe(EUIButtonType buttonType, UnityAction callback) =>
        GetButton(buttonType).AddListener(callback);

    private void RegisterButton()
    {
        _buttonGroup = new Dictionary<EUIButtonType, UIButton>();
        var uiButtons = GetComponentsInChildren<UIButton>();

        foreach (var uiButton in uiButtons)
        {
            uiButton.Initialize();

            _buttonGroup[uiButton.ButtonType] = uiButton;
        }
    }

    private UIButton GetButton(EUIButtonType buttonType)
    {
        if (_buttonGroup.ContainsKey(buttonType))
            return _buttonGroup[buttonType];
        
        return null;
    }
}
