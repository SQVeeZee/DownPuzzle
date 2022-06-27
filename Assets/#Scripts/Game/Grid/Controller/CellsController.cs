using System;
using UnityEngine;

public class CellsController : IDisposable
{
    private readonly CellsMoveController _cellsMoveController;

    private ElementsSelectionController _elementsSelectionController = null;
    
    private LevelsGridContainer _levelsGridContainer = null;
    
    private ClickHandler _clickHandler = null;

    private LevelCompleteController _levelCompleteController = null;

    public CellsController()
    {
        _cellsMoveController = new CellsMoveController();
    }
    
    public void Initialize(
        ClickHandler clickHandler, LevelsGridContainer levelsGridContainer, 
        LevelCompleteController levelCompleteController, 
        MovePattern movePattern, ScoreSystem scoreSystem)
    {
        _elementsSelectionController = new ElementsSelectionController(scoreSystem);
        
        _clickHandler = clickHandler;

        _levelsGridContainer = levelsGridContainer;
        
        _cellsMoveController.Initialize(_levelsGridContainer.GetGrid(), movePattern);
        
        _levelCompleteController = levelCompleteController;
        
        _cellsMoveController.onMoveCompleted += OnMoveCompleted;
        _elementsSelectionController.onDisablingCells += MoveCells;

        _clickHandler.onClick += OnClick;
    }

    public void Dispose()
    {
        _cellsMoveController.onMoveCompleted -= OnMoveCompleted;
        _elementsSelectionController.onDisablingCells -= MoveCells;

        _clickHandler.onClick -= OnClick;
    }

    private void MoveCells() => _cellsMoveController.MoveCells();
    private void OnMoveCompleted() => _levelCompleteController.CheckLevelState(_levelsGridContainer.GetFilledCells());

    private void OnClick(Vector2 worldPosition, float clickDistance)
    {
        if (_levelsGridContainer.TryGetCellByGlobalPosition(worldPosition, clickDistance, out GridCell gridCell))
        {
            _elementsSelectionController.DefineRuleBasedOnClickedCell(gridCell);
        }
    }
}