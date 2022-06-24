using UnityEngine;

[CreateAssetMenu(fileName = "GeneralGameConfigs", menuName = "ScriptableObjects/GENERAL/GeneralGameConfigs", order = 0)]
public class GeneralGameConfigs : ScriptableObject
{
    [Header("Level")]
    [SerializeField] private GeneralLevelConfigs _generalLevelConfigs = null;

    [Header("UI")]
    [SerializeField] private GeneralUIConfigs _generalUIConfigs = null;

    public GeneralLevelConfigs GeneralLevelConfigs => _generalLevelConfigs;
    public GeneralUIConfigs GeneralUIConfigs => _generalUIConfigs;
}
