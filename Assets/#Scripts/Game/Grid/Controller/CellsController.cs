using System;
using UnityEngine;

public class CellsController : MonoBehaviour, IDisposable
{
    private readonly CellsMoveController _cellsMoveController;
    private readonly ElementsSelectionController _elementsSelectionController = null;
    
    [SerializeField] private LevelsGridContainer _levelsGridContainer = null;
    
    [Header("Handler")]
    [SerializeField] private ClickHandler _clickHandler = null;

    private LevelCompleteController _levelCompleteController = null;

    private CellsController()
    {
        _cellsMoveController = new CellsMoveController();
        _elementsSelectionController = new ElementsSelectionController();
    }
    
    public void Initialize(LevelCompleteController levelCompleteController, MovePattern movePattern)
    {
        _cellsMoveController.Initialize(_levelsGridContainer.GetGrid(), movePattern);
        
        _levelCompleteController = levelCompleteController;
        
        _elementsSelectionController.onDisablingCells += MoveCells;
    }
    
    public void Dispose() => _elementsSelectionController.onDisablingCells -= MoveCells;

    
    private void OnEnable()
    {
        Subscribe();
    }

    private void OnDisable()
    {
        Unsubscribe();
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
    
    void Subscribe()
    {
        _cellsMoveController.onMoveCompleted += OnMoveCompleted;
        
        _clickHandler.onClick += OnClick;
    }
    
    void Unsubscribe()
    {
        _cellsMoveController.onMoveCompleted -= OnMoveCompleted;
        
        _clickHandler.onClick -= OnClick;
    }
}