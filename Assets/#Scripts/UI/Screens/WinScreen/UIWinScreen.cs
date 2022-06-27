using UniRx.Extensions;
using UnityEngine;
using UnityEngine.UI;

public class UIWinScreen : BaseScreen
{
    [Header("Buttons")]
    [SerializeField] private Button _nextButton = null;
    
    void Awake()
    {
        AddButtonsListener();

    }

    private void AddButtonsListener()
    {
        _nextButton.onClick.AddListener(OnNextButtonClick);
    }

    private void OnNextButtonClick()
    {
        
    }
}
