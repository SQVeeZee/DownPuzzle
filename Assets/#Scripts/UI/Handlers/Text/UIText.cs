using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UIText : MonoBehaviour
{
    [SerializeField] private UITextHandler.EUITextType _textType;

    public UITextHandler.EUITextType TextType => _textType;

    public TextMeshProUGUI Text { get; private set; }

    public void Initialize()
    {
        Text = GetComponent<TextMeshProUGUI>();
    }
}
