using System.Collections.Generic;
using TMPro;
using UniRx;
using UniRx.Extensions;
using UnityEngine;

public class UITextHandler : MonoBehaviour,IUIElementsHandler
{
    public enum EUITextType
    {
        NONE = 0,
        
        GAME_SCORE = 10,
    }

    private Dictionary<EUITextType, UIText> _textGroup;

    public void Register()
    {
        _textGroup = new Dictionary<EUITextType, UIText>();
        var uiTexts = GetComponentsInChildren<UIText>();

        foreach (var uiText in uiTexts)
        {
            uiText.Initialize();

            _textGroup[uiText.TextType] = uiText;
        }
    }

    public void Subscribe<T>(EUITextType textType, ReactiveProperty<T> value) =>
        value.SubscribeToText(GetText(textType));

    private TextMeshProUGUI GetText(EUITextType textType)
    {
        if (_textGroup.ContainsKey(textType))
            return _textGroup[textType].Text;
        
        return null;
    }
}
