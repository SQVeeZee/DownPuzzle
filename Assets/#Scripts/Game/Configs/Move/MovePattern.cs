using UnityEngine;

[CreateAssetMenu(fileName = "MovePattern", menuName = "ScriptableObjects/Move/MovePattern", order = 2)]
public class MovePattern : ScriptableObject
{
    [SerializeField] private VerticalMovePattern _verticalMovePattern = null;
    [SerializeField] private HorizontalMovePattern _horizontalMovePattern = null;
    
    public VerticalMovePattern VerticalMovePattern => _verticalMovePattern;
    public HorizontalMovePattern HorizontalMovePattern => _horizontalMovePattern;
}
