using UnityEngine;

public class BorderController : MonoBehaviour
{
    [SerializeField] private Border _border = null;

    [SerializeField] private GridBorderConfigs _borderConfigs = null;
    
    public void EnableBorder(ExtremePointsDetection extremePointsDetection)
    {
        var extremePoints = extremePointsDetection.ExtremeWorldPoints;

        _border.SetupLineRenderer(extremePoints, _borderConfigs);
    }
}
