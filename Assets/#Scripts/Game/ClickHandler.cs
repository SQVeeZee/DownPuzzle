using System;

public class ClickHandler: IDisposable
{
    public event Action<UnityEngine.Vector2, float> onClick = default;

    private readonly MobileInputController _mobileInputController = null;
    private readonly ICameraItem _gameCamera = null;
    private readonly float _clickDistance = default;

    public ClickHandler(CellSetting cellSetting)
    {
        _clickDistance = cellSetting.CellSize.x / 2;
        
        _mobileInputController = MobileInputController.Instance;
        _gameCamera = CameraManager.Instance.GetCameraItem(ECameraType.GAME);
    }

    public void Initialize() => _mobileInputController.onPointerDown += DetectClickedCell;

    public void Dispose() => _mobileInputController.onPointerDown -= DetectClickedCell;
    
    private void DetectClickedCell(UnityEngine.Vector2 inputPosition)
    {
        var worldPosition = _gameCamera.ConvertScreenToWorldPoint(inputPosition);
        
        onClick?.Invoke(worldPosition, _clickDistance);
    }
}