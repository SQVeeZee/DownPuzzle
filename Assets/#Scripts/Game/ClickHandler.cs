using System;
using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    public event Action<Vector2, float> onClick = default;

    private MobileInputController _mobileInputController = null;
    private ICameraItem _gameCamera = null;

    private float _clickDistance = default;

    private void Awake()
    {
        SetData();
    }
    
    public void SetClickDistance(CellSetting cellSetting) => _clickDistance = cellSetting.CellSize.x / 2;

    private void SetData()
    {
        _mobileInputController = MobileInputController.Instance;
        
        _gameCamera = CameraManager.Instance.GetCameraItem(ECameraType.GAME);
    }
    
    private void DetectClickedCell(Vector2 inputPosition)
    {
        var worldPosition = _gameCamera.ConvertScreenToWorldPoint(inputPosition);
        
        onClick?.Invoke(worldPosition, _clickDistance);
    }
    
    private void OnEnable()
    {
        Subscribe();   
    }

    private void OnDisable()
    {
        UnSubscribe();
    }

    private void Subscribe()
    {
        _mobileInputController.onPointerDown += DetectClickedCell;
    }

    private void UnSubscribe()
    {
        _mobileInputController.onPointerDown -= DetectClickedCell;
    }
}