using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelItem", menuName = "ScriptableObjects/Levels/LevelItem", order = 1)]
public class LevelItemConfigs : ScriptableObject
{
    [SerializeField] private GridSize _gridSize;

    public GridSize GridSize => _gridSize;
}
