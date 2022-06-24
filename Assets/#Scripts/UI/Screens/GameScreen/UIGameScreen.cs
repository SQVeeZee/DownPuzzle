using UnityEngine;
using TMPro;

public class UIGameScreen : BaseScreen
{
    [Header("Score")]
    [SerializeField] private TextMeshProUGUI _scoreText = null;
    
    private void OnEnable()
    {
        Subscribe();
    }
    private void OnDisable()
    {
        Unsubscribe();
    }
    
    private void SetScoreText(int score) => _scoreText.text = score.ToString(); 

    
    private void Subscribe()
    {
        ScoreSystem.onUpdateScore += SetScoreText;
    }

    private void Unsubscribe()
    {
        ScoreSystem.onUpdateScore -= SetScoreText;
    }
}
