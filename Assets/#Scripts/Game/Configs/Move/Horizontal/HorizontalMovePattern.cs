using UnityEngine;

[CreateAssetMenu(fileName = "HorizontalMovePattern", menuName = "ScriptableObjects/Move/HorizontalMovePattern", order = 1)]
public class HorizontalMovePattern : BaseDirectionMovePattern
{
    [SerializeField] private EHorizontalDirectionType _horizontalDirectionType;
    
    public EHorizontalDirectionType HorizontalDirectionType => _horizontalDirectionType;
}
