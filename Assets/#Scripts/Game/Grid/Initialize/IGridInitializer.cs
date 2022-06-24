using System;

public interface IGridInitializer
{
    event Action<GridCell[,]> onGridCreated;
}
