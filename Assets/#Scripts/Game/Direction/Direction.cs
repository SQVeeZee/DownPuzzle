using System;
using UnityEngine;

public struct Direction
{
    public EDirectionType DirectionType;
    
    public bool TryGetGridLocalPositionByDirectionType(CellsLocalPosition cellsLocalPosition, EDirectionType directionType, Action<CellsLocalPosition> callback)
    {
        Vector2 localPosition = new Vector2(cellsLocalPosition.PositionX, cellsLocalPosition.PositionY);

        switch (directionType)
        {
            case EDirectionType.RIGHT: 
            return OnGetLocalPosition(ConvertVectorToGridPosition(localPosition + Vector2.right));
            case EDirectionType.BOTTOM: 
            return OnGetLocalPosition(ConvertVectorToGridPosition(localPosition + Vector2.down));
            case EDirectionType.LEFT: 
            return OnGetLocalPosition(ConvertVectorToGridPosition(localPosition + Vector2.left));
            case EDirectionType.TOP: 
            return OnGetLocalPosition(ConvertVectorToGridPosition(localPosition + Vector2.up));
        }

        return false;
        
        bool OnGetLocalPosition(CellsLocalPosition cellsLocalPosition)
        {
            callback?.Invoke(cellsLocalPosition);
            return true;
        }
    }

    private CellsLocalPosition ConvertVectorToGridPosition(Vector2 localPosition)
    {
        CellsLocalPosition gridPosition = new CellsLocalPosition();

        gridPosition.PositionX = (int)localPosition.x;
        gridPosition.PositionY = (int)localPosition.y;

        return gridPosition;
    }
}
