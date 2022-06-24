using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BorderConfig", menuName = "ScriptableObjects/Grid/BorderConfig", order = 1)]
public class GridBorderConfigs : ScriptableObject
{
    [SerializeField] private float _borderSize = default;

    public float BorderSize => _borderSize;
}
