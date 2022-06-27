using System.Collections.Generic;
using UnityEngine;

public class ArrowsCreator : MonoBehaviour
{
    [SerializeField] private VisualArrow _arrowPrefab = null;

    private readonly Stack<VisualArrow> _arrows = new Stack<VisualArrow>();
    
    private CalculateArrows _calculateArrows = null;
    
    public void Initialize(GridSize gridSize)
    {
        _calculateArrows = new CalculateArrows();

        _calculateArrows.CalculateArrowsCount(gridSize.Height);
    }
    
    public void CreateArrows(int arrowsCount)
    {
        Vector2 position = transform.position;
        
        for (int i = 0; i < arrowsCount; i++)
        {
            _arrows.Push(CreateArrow(position));

            // IncreasePositionByDirection(ref position);
        }
    }

    public void ClearArrows()
    {
        for (int i = 0; i < _arrows.Count; i++)
        {
            _arrows.Pop().DestroyArrow();
        }
    }
    
    private void IncreasePositionByDirection(ref Vector2 position)
    {
        var increasePosition = _calculateArrows.DistanceBetweenArrows;
        
        position.x += increasePosition;
    }
    
    private VisualArrow CreateArrow(Vector2 position) => Instantiate(_arrowPrefab, position, Quaternion.identity, transform);
}
