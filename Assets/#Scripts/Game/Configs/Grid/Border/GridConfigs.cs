using UnityEngine;

[CreateAssetMenu(fileName = "GridConfigs", menuName = "ScriptableObjects/Grid/GridConfigs", order = 1)]
public class GridConfigs : ScriptableObject
{
    [SerializeField] private float _clampX = 0.1f;
    [SerializeField] private float _minClampY = 0.6f;
    [SerializeField] private float _maxClampY = .9f;

    public float RightScreenPos => Screen.width - Screen.width * _clampX;
    public float BottomScreenPos => Screen.height * _minClampY;
    public float TopScreenPos => Screen.height * _maxClampY;
}
