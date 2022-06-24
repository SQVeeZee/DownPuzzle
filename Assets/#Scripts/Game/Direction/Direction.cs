using UnityEngine;

public static class Direction
{
    public static Position GetGridLocalPositionByDirectionType(CellPosition cellPosition, EDirectionType directionType)
    {
        Vector2 localPosition = cellPosition.GetVector2LocalPosition();

        Vector2 directionVector = directionType switch
        {
            EDirectionType.RIGHT => Vector2.right,
            EDirectionType.BOTTOM => Vector2.down,
            EDirectionType.LEFT => Vector2.left,
            EDirectionType.TOP => Vector2.up,
            
            _ => Vector2.zero
        };
        
        localPosition += directionVector;

        Position position = new Position(localPosition);

        return position;
    }
}
