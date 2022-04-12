using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelsConfigs", menuName = "ScriptableObjects/Levels/LevelsConfigs", order = 1)]
public class LevelsConfigs : ScriptableObject
{
    [SerializeField] private List<LevelItemConfigs> _levelItems = new List<LevelItemConfigs>();

    public List<LevelItemConfigs> LevelItems => _levelItems;
}
