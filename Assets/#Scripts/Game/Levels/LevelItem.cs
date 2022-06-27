using System;
using UnityEngine;

public class LevelItem : MonoBehaviour, IDisposable
{
    public event Action<ELevelCompleteReason> onLevelComplete = default;

    [Header("Border")]
    [SerializeField] private BorderController _borderController = null;

    private readonly LevelCompleteController _levelCompleteController = null;
    private readonly CellsController _cellsController = null;
    
    private ClickHandler _clickHandler = null;
    private LevelItem()
    {
        _levelCompleteController = new LevelCompleteController();
        _cellsController = new CellsController();
    }
    
    private void OnEnable()
    {
        Subscribe();
    }
    private void OnDisable()
    {
        UnSubscribe();
    }

    public void Initialize(GeneralLevelConfigs generalLevelConfigs, LevelItemConfigs levelItemConfigs, ElementsFactory elementsFactory, ReactivePresenter reactivePresenter)
    {
        var cellSizeDetection = InitializeCellSizeDetection(generalLevelConfigs.GridConfigs, levelItemConfigs.GridSize);
        var extremePointsDetection = InitializeExtremePointsDetection(generalLevelConfigs.GridConfigs, levelItemConfigs.GridSize, cellSizeDetection.CellSetting);
        _borderController.EnableBorder(extremePointsDetection);

        var gridInitializer = new GridInitializer(elementsFactory, extremePointsDetection, levelItemConfigs, cellSizeDetection.CellSetting);
        var gridContainer = new LevelsGridContainer(gridInitializer);
        gridInitializer.InitializeGrid();

        _clickHandler = new ClickHandler(cellSizeDetection.CellSetting);
        
        _cellsController.Initialize(_clickHandler, gridContainer, _levelCompleteController, levelItemConfigs.MovePattern, reactivePresenter.ScoreSystem);
        
        _clickHandler.Initialize();
    }
    
    private ExtremePointsDetection InitializeExtremePointsDetection(GridConfigs gridConfigs, GridSize gridSize, CellSetting cellSetting) => 
        new ExtremePointsDetection(gridConfigs, gridSize, cellSetting);

    private CellSizeDetection InitializeCellSizeDetection(GridConfigs gridConfigs, GridSize gridSize) =>
        new CellSizeDetection(gridConfigs, gridSize);


    private void OnLevelCompleted(ELevelCompleteReason levelCompleteReason) =>
        onLevelComplete?.Invoke(levelCompleteReason);
    
    private void Subscribe()
    {
        _levelCompleteController.onLevelCompleted += OnLevelCompleted;
    }
    private void UnSubscribe()
    {
        _levelCompleteController.onLevelCompleted -= OnLevelCompleted;
    }

    public void Dispose()
    {
        _clickHandler.Dispose();
        
        _cellsController.Dispose();
    }
}