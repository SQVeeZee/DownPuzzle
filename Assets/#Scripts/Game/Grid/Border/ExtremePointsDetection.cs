using UnityEngine;

public class ExtremePointsDetection
{
    private readonly ICameraItem _gameCamera = null;
    private readonly GridConfigs _gridConfigs = null;
    private readonly CellSetting _cellSetting = default;

    public Vector2[,] ExtremeWorldPoints { get; }
    
    ExtremePointsDetection()
    {
        _gameCamera = CameraManager.Instance.GetCameraItem(ECameraType.GAME);
    }

    public ExtremePointsDetection(GridConfigs gridConfigs, GridSize gridSize, CellSetting cellSetting): this()
    {
        _gridConfigs = gridConfigs;
        _cellSetting = cellSetting;

        ExtremeWorldPoints = GetExtremeWorldPoints(gridSize);
    }
    
    private Vector2[,] GetExtremeWorldPoints(GridSize gridSize)
    {
        Vector2[,] extremeScreenPoints = new Vector2[2, 2];
        
        float topWorldPos = _gameCamera.ConvertScreenToWorldPoint(Vector2.one * Mathf.Abs(_gridConfigs.TopScreenPos)).y;
        float bottomWorldPos = _gameCamera.ConvertScreenToWorldPoint(Vector2.one * Mathf.Abs(_gridConfigs.BottomScreenPos)).y;

        float middleVerticalPoint = (topWorldPos + bottomWorldPos) / 2;

        float leftScreenPos = -((float)gridSize.Width / 2) * _cellSetting.CellSizeMultiply;
        float rightScreenPos = leftScreenPos + gridSize.Width * _cellSetting.CellSizeMultiply;

        float bottomScreenPos = middleVerticalPoint - ((float)gridSize.Height + 1) / 2 * _cellSetting.CellSizeMultiply;
        float topScreenPos = bottomScreenPos + gridSize.Height * _cellSetting.CellSizeMultiply;

        extremeScreenPoints[0, 0] = GetExtremePoint(leftScreenPos, bottomScreenPos);
        extremeScreenPoints[1, 0] = GetExtremePoint(rightScreenPos, bottomScreenPos);
        extremeScreenPoints[1, 1] = GetExtremePoint(rightScreenPos, topScreenPos);
        extremeScreenPoints[0, 1] = GetExtremePoint(leftScreenPos, topScreenPos);

        return extremeScreenPoints;
    }

    private static Vector2 GetExtremePoint(float horizontalPos, float verticalPos) => new Vector2
    {
        x = horizontalPos,
        y = verticalPos
    };
}
