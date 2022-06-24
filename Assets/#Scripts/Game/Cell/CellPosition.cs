using UnityEngine;

public struct CellPosition
{
    public Vector2 GlobalPosition;
    public Position LocalPosition;

    public Vector2 GetVector2LocalPosition() => new Vector2(LocalPosition.PositionX, LocalPosition.PositionY);

    public Position GetPosition(Vector2 vector2) => new Position(vector2);
}

public struct Position
{
    public readonly int PositionX;
    public readonly int PositionY;

    public Position(int x, int y)
    {
        PositionX = x;
        PositionY = y;
    }

    public Position(float x, float y)
    {
        PositionX = (int) x;
        PositionY = (int) y;
    }

    public Position(Vector2 localPosition)
    {
        PositionX = (int) localPosition.x;
        PositionY = (int) localPosition.y;
    }
    
    public static Vector2 operator +(Position position, Vector2 vector2) => 
        new Vector2(position.PositionX, position.PositionY) + vector2;
}
