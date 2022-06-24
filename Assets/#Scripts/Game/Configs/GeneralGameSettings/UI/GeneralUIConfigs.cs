using UnityEngine;

[CreateAssetMenu(fileName = "GeneralUIConfigs", menuName = "ScriptableObjects/GENERAL/UI/GeneralUIConfigs", order = 0)]
public class GeneralUIConfigs : ScriptableObject
{
    [SerializeField] private float _showTime = 0.5f;
    [SerializeField] private float _hideTime = 0.5f;

    public float ShowTime => _showTime;
    public float HideTime => _hideTime;
}
