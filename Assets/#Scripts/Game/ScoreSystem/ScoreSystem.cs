using UniRx;

public class ScoreSystem
{
    public ScoreSystem(int score)
    {
        CurrentScore = new ReactiveProperty<int>(score);
    }

    public ScoreSystem()
    {
        CurrentScore = new ReactiveProperty<int>(0);
    }
    
    public ReactiveProperty<int> CurrentScore { get; }

    public void ResetScore() => CurrentScore.Value = 0;

    public void UpdateScore(int score)
    {
        CurrentScore.Value += CalculateExtraScore(score);
    }

    private int CalculateExtraScore(int score)
    {

        return score;
    }
}
