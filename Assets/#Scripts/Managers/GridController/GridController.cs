using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : Singleton<GridController>
{
    [SerializeField] private GridConfigs _gridConfigs = null;
    [SerializeField] private GridBorderConfigs _borderConfigs = null;

    public GridConfigs GridConfigs => _gridConfigs;
    public GridBorderConfigs BorderConfigs => _borderConfigs;
}
