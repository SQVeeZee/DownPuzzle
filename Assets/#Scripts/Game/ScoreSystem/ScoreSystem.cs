using System;

public static class ScoreSystem
{
    public static event Action<int> onUpdateScore;

    private static int _currentScore = 0;

    private static void ResetScore() => _currentScore = 0;

    public static void UpdateScore(int score)
    {
        _currentScore += CalculateExtraScore(score);
        
        onUpdateScore?.Invoke(_currentScore);
    }

    private static int CalculateExtraScore(int score)
    {

        return score;
    }
}
