using UnityEngine;

[CreateAssetMenu(fileName = "GeneralLevelConfigs", menuName = "ScriptableObjects/GENERAL/Level/GeneralLevelConfigs", order = 0)]
public class GeneralLevelConfigs : ScriptableObject
{
    [Header("GridConfigs")] 
    [SerializeField] private GridConfigs _gridConfigs = null;

    [Header("Match")] 
    [SerializeField] private MatchElementConfigs _matchElementConfigs = null; 
    
    public GridConfigs GridConfigs => _gridConfigs;
    public MatchElementConfigs MatchElementConfigs => _matchElementConfigs;
}
