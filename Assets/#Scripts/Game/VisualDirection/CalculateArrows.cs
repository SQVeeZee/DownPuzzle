public class CalculateArrows
{
    private float _ditanceBetweenArrows = 0.2f;
    private float _arrowSize = 0.5f;

    public float DistanceBetweenArrows => _arrowSize + _ditanceBetweenArrows;
    
    public CalculateArrows()
    {
        _arrowSize = GetArrowSize();
    }
    
    public int CalculateArrowsCount(float gridLength) => 
        (int)(gridLength / (_arrowSize + _ditanceBetweenArrows));

    private float GetArrowSize()
    {
        return 0.3f;
    }
}
