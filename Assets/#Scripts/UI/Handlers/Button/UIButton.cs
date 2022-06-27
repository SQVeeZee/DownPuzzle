using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIButton : MonoBehaviour
{
    [SerializeField] private UIButtonHandler.EUIButtonType _buttonType;

    public UIButtonHandler.EUIButtonType ButtonType => _buttonType;

    private Button _button = null;

    public void Initialize()
    {
        _button = GetComponent<Button>();
    }

    public void AddListener(UnityAction callback) => 
        _button.OnClickAsObservable().Subscribe(_ => callback());
}
