using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraItem : MonoBehaviour, ICameraItem
{
    [SerializeField] private ECameraType _cameraType = ECameraType.NONE;
    [SerializeField] private Camera _camera = null;

    public ECameraType CameraType { get => _cameraType; }
    public Camera Camera => _camera;

}
