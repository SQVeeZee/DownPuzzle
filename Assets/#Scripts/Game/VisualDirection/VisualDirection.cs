using UnityEngine;

public class VisualDirection : MonoBehaviour
{
    [SerializeField] private ArrowsCreator _arrowsCreator = null;

    private CalculateArrows _calculateArrows = null;

    public void Initialize(GridSize gridSize)
    {
        _calculateArrows = new CalculateArrows();

        _calculateArrows.CalculateArrowsCount(gridSize.Height);
    }

    private void DefineGridSize(MovePattern movePattern)
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

    public void CreateArrows(int arrowsCount)
    {
        Vector2 position = transform.position;
        
        for (int i = 0; i < arrowsCount; i++)
        {
            _arrowsCreator.CreateArrow(position);

            IncreasePositionByDirection(ref position);
        }
    }

    private void IncreasePositionByDirection(ref Vector2 position)
    {
        var increasePosition = _calculateArrows.DistanceBetweenArrows;
        
        position.x += increasePosition;
    }
    
    public void EnableDirection()
    {
        
    }

    public void DisableDirection()
    {
        
    }
}
