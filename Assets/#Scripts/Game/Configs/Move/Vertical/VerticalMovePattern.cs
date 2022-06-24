using UnityEngine;

[CreateAssetMenu(fileName = "VerticalMovePattern", menuName = "ScriptableObjects/Move/VerticalMovePattern", order = 1)]
public class VerticalMovePattern : BaseDirectionMovePattern
{
    [SerializeField] private EVerticalDirectionType _verticalDirectionType;
    
    public EVerticalDirectionType VerticalDirectionType => _verticalDirectionType;
}
