using UnityEngine;

[CreateAssetMenu(fileName = "LevelItem", menuName = "ScriptableObjects/Levels/LevelItem", order = 1)]
public class LevelItemConfigs : ScriptableObject
{
    [Header("Move Pattern")] 
    [SerializeField] private MovePattern _movePattern = null; 
    
    [Header("Size")]
    [SerializeField] private GridSize _gridSize;

    public GridSize GridSize => _gridSize;
    public MovePattern MovePattern => _movePattern;
}
