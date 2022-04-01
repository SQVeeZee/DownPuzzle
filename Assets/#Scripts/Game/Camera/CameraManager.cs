using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    [SerializeField] private List<CameraItem> _cameraItems = new List<CameraItem>();

    public List<CameraItem> CameraItems => _cameraItems;

    public ICameraItem GetCameraItem(ECameraType cameraType)
    {
        return CameraItems.Find(item => item.CameraType == cameraType);
    }
}
