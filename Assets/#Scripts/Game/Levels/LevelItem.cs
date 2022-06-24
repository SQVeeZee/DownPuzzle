using System;
using UnityEngine;

public class LevelItem : MonoBehaviour, IDisposable
{
    public event Action<ELevelCompleteReason> onLevelComplete = default;

    private readonly LevelCompleteController _levelCompleteController = null;
    
    [Header("Grid")]
    [SerializeField] private GridInitializer _gridInitializer = null;
    
    [Header("Cells")]
    [SerializeField] private CellsController _cellsController = null;

    [Header("Border")]
    [SerializeField] private BorderController borderController = null;

    [Header("ClickHandler")] 
    [SerializeField] private ClickHandler _clickHandler = null;
    
    private GeneralLevelConfigs _generalLevelConfigs = null;
    
    private LevelItem()
    {
        _levelCompleteController = new LevelCompleteController();
    }
    
    private void OnEnable()
    {
        Subscribe();
    }
    private void OnDisable()
    {
        UnSubscribe();
    }

    public void Initialize(GeneralLevelConfigs generalLevelConfigs, LevelItemConfigs levelItemConfigs)
    {
        InitializeLevelElements(generalLevelConfigs, levelItemConfigs);
    }

    private void InitializeLevelElements(GeneralLevelConfigs generalLevelConfigs, LevelItemConfigs levelItemConfigs)
    {
        var cellSizeDetection = InitializeCellSizeDetection(generalLevelConfigs.GridConfigs, levelItemConfigs.GridSize);
        var extremePointsDetection = InitializeExtremePointsDetection(generalLevelConfigs.GridConfigs, levelItemConfigs.GridSize, cellSizeDetection.CellSetting);
        
        _gridInitializer.InitializeGrid(extremePointsDetection, levelItemConfigs, cellSizeDetection.CellSetting);
        
        _cellsController.Initialize(_levelCompleteController, levelItemConfigs.MovePattern);
        
        SetupClickHandler(cellSizeDetection.CellSetting);

        borderController.EnableBorder(extremePointsDetection);
    }

    private ExtremePointsDetection InitializeExtremePointsDetection(GridConfigs gridConfigs, GridSize gridSize, CellSetting cellSetting) => 
        new ExtremePointsDetection(gridConfigs, gridSize, cellSetting);

    private CellSizeDetection InitializeCellSizeDetection(GridConfigs gridConfigs, GridSize gridSize) =>
        new CellSizeDetection(gridConfigs, gridSize);


    private void SetupClickHandler(CellSetting cellSetting) => _clickHandler.SetClickDistance(cellSetting);

    private void OnLevelCompleted(ELevelCompleteReason levelCompleteReason) => onLevelComplete?.Invoke(levelCompleteReason);
    
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
        _cellsController.Dispose();
    }
}