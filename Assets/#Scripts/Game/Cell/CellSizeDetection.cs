using UnityEngine;

public struct CellSetting
{
    public Vector2 CellSize => Vector2.one * CellSizeMultiply;
    public float CellSizeMultiply { get; }

    public CellSetting(float multiply)
    {
        CellSizeMultiply = Mathf.Abs(multiply);
    }
}

public class CellSizeDetection
{
    public CellSetting CellSetting { get; }

    private GridConfigs _gridConfigs = null;
    private GridSize _gridSize = default;

    private ICameraItem _gameCamera;

    private CellSizeDetection()
    {
        _gameCamera = CameraManager.Instance.GetCameraItem(ECameraType.GAME);
    }
    
    public CellSizeDetection(GridConfigs gridConfigs, GridSize gridSize): this()
    {
        _gridConfigs = gridConfigs;
        _gridSize = gridSize;

        CellSetting = GetCellSizeSetting();
    }
    
    private CellSetting GetCellSizeSetting()
    {
        float rightWorldPos = _gameCamera.ConvertScreenToWorldPoint(Vector2.one * _gridConfigs.RightScreenPos).x;
        float topWorldPos = _gameCamera.ConvertScreenToWorldPoint(Vector2.one * Mathf.Abs(_gridConfigs.TopScreenPos)).y;
        float bottomWorldPos = _gameCamera.ConvertScreenToWorldPoint(Vector2.one * Mathf.Abs(_gridConfigs.BottomScreenPos)).y;

        float widthDistance = rightWorldPos * 2;
        float heightDistance = topWorldPos - bottomWorldPos;

        float cellSizeByWidth = widthDistance / _gridSize.Width;
        float cellSizeByHeight = heightDistance / _gridSize.Height;

        float cellSizeMultiply = cellSizeByWidth < cellSizeByHeight ? cellSizeByWidth : cellSizeByHeight;

        return new CellSetting(cellSizeMultiply);
    }
}
