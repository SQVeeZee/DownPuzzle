using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIGameScreen : BaseScreen
{
    [Header("Score")]
    [SerializeField] private TextMeshProUGUI _scoreText = null;
    
    private LevelsController _levelsController = null;

    private int _currentScore = 0;

    void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        _levelsController = LevelsController.Instance;
    }

    void OnEnable()
    {
        _levelsController.onLevelItemSet += SubscribeOnLevelEvent;
        _levelsController.onLevelItemDispose += UnSubscribeOnLevelEvent;
    }

    void OnDisable()
    {
        _levelsController.onLevelItemSet -= SubscribeOnLevelEvent;
        _levelsController.onLevelItemDispose -= UnSubscribeOnLevelEvent;
    }
    
    private void SubscribeOnLevelEvent(LevelItem levelItem)
    {
        ResetScore();

        levelItem.onDisableCells += UpdateScore;
    }

    private void UnSubscribeOnLevelEvent(LevelItem levelItem)
    {
        levelItem.onDisableCells -= UpdateScore;
    }

    private void UpdateScore(int score)
    {
        _currentScore += score;

        SetScoreText(_currentScore);
    }

    private void ResetScore()
    {
        _currentScore = 0;

        SetScoreText(_currentScore);
    }

    private void SetScoreText(int score)
    {
        string scoreText = _currentScore.ToString();
        
        _scoreText.text = scoreText; 
    }
}
