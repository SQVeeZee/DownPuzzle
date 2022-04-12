using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GridConfigs", menuName = "ScriptableObjects/Grid/GridConfigs", order = 1)]
public class GridConfigs : ScriptableObject
{
    [SerializeField] private float _clampX = 0.1f;
    [SerializeField] private float _minClampY = 0.6f;
    [SerializeField] private float _maxClampY = .9f;

    public float СlampX => _clampX;
    public float MinClampY => _minClampY;
    public float MaxClampY => _maxClampY;
}
