using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWinScreen : BaseScreen
{
    [Header("Buttons")]
    [SerializeField] private Button _nextButton = null;
    
    private LevelsController _levelsController = null;

    void Awake()
    {
        Initialize();

        AddButtonsListener();
    }

    private void Initialize()
    {
        _levelsController = LevelsController.Instance;
    }

    private void AddButtonsListener()
    {
        _nextButton.onClick.AddListener(StartNextLevel);
    }

    private void StartNextLevel()
    {
        _levelsController.GoToNextLevel();
    }
}
