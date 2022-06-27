using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Configs")]
    [SerializeField] private GeneralGameConfigs _generalGameConfigs = null;
    
    [Space(10)]
    [SerializeField] private LevelsController _levelsController = null;
    [SerializeField] private UIManager _UIManager = null;

    [Space(10)] 
    [SerializeField] private ReactivePresenter _reactivePresenter = null;

    #region MONO_BEHAVIOUR

    void Awake()
    {
        SetupUI();
        SetupLevelsController();
        
        SetupReactivePresenter();
    }

    private void Start()
    {
        _UIManager.DisableScreens();
        
        StartLevel();
    }
    
    private void OnEnable()
    {
        Subscribe();
    }
    private void OnDisable()
    {
        Unsubscribe();
    }

    #endregion

    #region UI

    private void SetupUI()
    {
        _UIManager.Initialize(_generalGameConfigs.GeneralUIConfigs);
    }

    #endregion
    
    #region LEVELS

    private void StartLevel() => _levelsController.CreateLevel();

    private void SetupLevelsController()
    {
        _levelsController.Initialize(_generalGameConfigs.GeneralLevelConfigs,_reactivePresenter);
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

    #endregion

    #region ReactivePresenter

    private void SetupReactivePresenter()
    {
        _reactivePresenter.Initialize(_levelsController);
    }

    #endregion
    
    #region EVENTS

    void Subscribe()
    {
        // ScoreSystem.onUpdateScore += UpdateScore;
        
        _levelsController.onLevelCompleted += OnLevelCompleted;
        _levelsController.onLevelItemSet += OnLevelCreated;
    }

    void Unsubscribe()
    {
        // ScoreSystem.onUpdateScore -= UpdateScore;
        
        _levelsController.onLevelCompleted -= OnLevelCompleted;
        _levelsController.onLevelItemSet -= OnLevelCreated;
    }

    #endregion
}
