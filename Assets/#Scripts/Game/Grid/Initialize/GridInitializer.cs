using System;
using UnityEngine;

public class GridInitializer: IGridInitializer
{
    public event Action<GridCell[,]> onGridCreated;

    private readonly ElementsFactory _elementsFactory = null;

    private readonly ExtremePointsDetection _extremePointsDetection = null;
    private readonly LevelItemConfigs _levelItemConfigs = null;
    
    private CellSetting _cellSetting;
    public GridInitializer(
        ElementsFactory elementsFactory,
        ExtremePointsDetection extremePointsDetection, 
        LevelItemConfigs levelItemConfigs, 
        CellSetting cellSetting)
    {
        _elementsFactory = elementsFactory;
        _extremePointsDetection = extremePointsDetection;
        _levelItemConfigs = levelItemConfigs;
        
        _cellSetting = cellSetting;
    }

    public void InitializeGrid()
    {
        GridSize gridSize = _levelItemConfigs.GridSize;
        
        if(!CanCreateGrid(gridSize)) return;
        
        EVerticalDirectionType verticalDirectionType = _levelItemConfigs.MovePattern.VerticalMovePattern.VerticalDirectionType;
        EHorizontalDirectionType horizontalDirectionType = _levelItemConfigs.MovePattern.HorizontalMovePattern.HorizontalDirectionType;
        
        GridCell [,] gridCells = new GridCell[gridSize.Width, gridSize.Height];

        Vector2 extremePoint = GetExtremePoint(_extremePointsDetection.ExtremeWorldPoints, verticalDirectionType, horizontalDirectionType);
        Vector2 displacement = GetDisplacement(_cellSetting, verticalDirectionType, horizontalDirectionType);
        
        Vector2 firstGlobalCellPosition = extremePoint + displacement;
        
        int verticalIncrease = GetSignForVerticalMove(verticalDirectionType);
        int horizontalIncrease = GetSignForHorizontalMove(horizontalDirectionType);
        
        for (int i = 0; i < gridSize.Width; i++)
        {
            for (int j = 0; j < gridSize.Height; j++)
            {
                Vector2 globalCellPosition = DefineGlobalPositionByIndexes(i, j, _cellSetting);
                
                CellPosition cellPosition = GetCellsPositions(globalCellPosition, new Vector2(i, j));
                GridCell gridCell = new GridCell(cellPosition);
                
                IElement element = _elementsFactory.CreateElement(EElementType.MOVABLE, globalCellPosition, _cellSetting.CellSize);
                gridCell.SetElement(element);
                
                gridCells[i, j] = gridCell;
            }
        }
        new CloseDirectionCells().SetCellsNeighbors(gridCells);
        
        onGridCreated?.Invoke(gridCells);
        
        Vector2 DefineGlobalPositionByIndexes(int horizontalId, int verticalId, CellSetting cellSetting)
        {
            var globalPosition = 
                firstGlobalCellPosition + new Vector2(horizontalId * horizontalIncrease, verticalId * verticalIncrease) * 
                cellSetting.CellSize;
            
            return globalPosition;
        }
    }
    
    private static bool CanCreateGrid(GridSize gridSize)
    {
        if (gridSize.Width > 0 && gridSize.Height > 0) return true;

        throw new Exception("Size of grid is incorrect");
    }
    
    private static Vector2 GetDisplacement(CellSetting cellSetting, EVerticalDirectionType verticalDirectionType, EHorizontalDirectionType horizontalDirectionType)
    {
        Vector2 displacement = cellSetting.CellSize / 2;
        
        return new Vector2
            (
            displacement.x * GetSignForHorizontalMove(horizontalDirectionType),
            displacement.y * GetSignForVerticalMove(verticalDirectionType)
            );
    }
    
    private Vector2 GetExtremePoint(Vector2[,] extremePoints, EVerticalDirectionType verticalDirectionType, EHorizontalDirectionType horizontalDirectionType)
    {
        int x = 0;
        int y = 0;

        x = horizontalDirectionType switch
        {
            EHorizontalDirectionType.RIGHT => 1,
            EHorizontalDirectionType.LEFT => 0,
            _ => x
        };

        y = verticalDirectionType switch
        {
            EVerticalDirectionType.TOP => 1,
            EVerticalDirectionType.DOWN => 0,
            _ => y
        };
        
        return extremePoints[x, y];
    }

    private static int GetSignForHorizontalMove(EHorizontalDirectionType horizontalDirectionType) => horizontalDirectionType switch
    {
        EHorizontalDirectionType.LEFT => 1,
        EHorizontalDirectionType.RIGHT => -1,
        _ => 1
    };
    
    private static int GetSignForVerticalMove(EVerticalDirectionType verticalDirectionType) => verticalDirectionType switch
    {
        EVerticalDirectionType.DOWN => 1,
        EVerticalDirectionType.TOP => -1,
        _ => 1
    };

    private static CellPosition GetCellsPositions(Vector2 globalPosition, Vector2 localPosition) => new CellPosition
    {
        LocalPosition = new Position(localPosition),
        GlobalPosition = globalPosition,
    };
}