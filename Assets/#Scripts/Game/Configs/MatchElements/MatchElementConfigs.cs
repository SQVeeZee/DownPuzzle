using UnityEngine;

[CreateAssetMenu(fileName = "MatchElementConfigs", menuName = "ScriptableObjects/GENERAL/Level/MatchElementConfigs", order = 0)]
public class MatchElementConfigs : ScriptableObject
{
    [SerializeField] private int _minElementsForMatch = 0;
    
    public int MinElementsForMatch => _minElementsForMatch;
}
