using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Configs")]
    [SerializeField] private GeneralGameConfigs _generalGameConfigs = null;
    
    [Space(10)]
    [SerializeField] private LevelsController _levelsController = null;
    [SerializeField] private UIManager _UIManager = null;
    
    void Awake()
    {
        SetupUI();
        SetupLevelsController();
    }

    private void Start()
    {
        _levelsController.CreateLevel();
    }

    private void SetupUI()
    {
        _UIManager.Initialize(_generalGameConfigs.GeneralUIConfigs);
        _UIManager.DisableScreens();
    }

    private void SetupLevelsController()
    {
        _levelsController.Initialize(_generalGameConfigs.GeneralLevelConfigs);
    }

    private void OnLevelCompleted(ELevelCompleteReason levelCompleteReason)
    {
        switch (levelCompleteReason)
        {
            case ELevelCompleteReason.WIN:
                _UIManager.ShowScreen(EScreenType.WIN);
                break;
            
            case ELevelCompleteReason.NONE:
            case ELevelCompleteReason.LOSE:
            default: 
                break;
        }
    }
    
    private void OnLevelCreated(LevelItem levelItem) => _UIManager.ShowScreen(EScreenType.GAME);

    void Subscribe()
    {
        _levelsController.onLevelCompleted += OnLevelCompleted;
        _levelsController.onLevelItemSet += OnLevelCreated;
    }

    void Unsubscribe()
    {
        _levelsController.onLevelCompleted -= OnLevelCompleted;
        _levelsController.onLevelItemSet -= OnLevelCreated;
    }

    private void OnEnable()
    {
        Subscribe();
    }

    private void OnDisable()
    {
        Unsubscribe();
    }
}
