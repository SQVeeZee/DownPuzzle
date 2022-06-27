using UnityEngine;

public class VisualDirection : MonoBehaviour
{
    [SerializeField] private ArrowsCreator _arrowsCreator = null;

    private CalculateArrows _calculateArrows = null;
        
    public void Initialize(GridSize gridSize)
    {
        _arrowsCreator.Initialize(gridSize);
    }

    public void EnableDirection()
    {
        
    }

    public void DisableDirection() => _arrowsCreator.ClearArrows();
    
    private void Define(MovePattern movePattern)
    {
        EHorizontalDirectionType horizontalDirection = movePattern.HorizontalMovePattern.HorizontalDirectionType;
        EVerticalDirectionType verticalDirection = movePattern.VerticalMovePattern.VerticalDirectionType;

        if (horizontalDirection != EHorizontalDirectionType.NONE)
        {
            
        }

        if (verticalDirection != EVerticalDirectionType.NONE)
        {
            
        }
    }
}
