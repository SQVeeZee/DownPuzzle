using System;
using UnityEngine;

public class LevelsController : MonoBehaviour
{
    public event Action<ELevelCompleteReason> onLevelCompleted = null;
    public event Action<LevelItem> onLevelItemSet = null;
    
    [SerializeField] private LevelItem _levelItemPrefab = null;
    [SerializeField] private ElementsFactory _elementsFactory = null;
    
    [Header("Configs")]
    [SerializeField] private LevelsConfigs _levelsConfigs = null;

    private ReactivePresenter _reactivePresenter = null;
    private GeneralLevelConfigs _generalLevelConfigs = null;
    
    private LevelItem _currentLevelItem = null;
    private int _currentLevelNumber = 0;

    public void Initialize(GeneralLevelConfigs generalLevelConfigs, ReactivePresenter reactivePresenter)
    {
        _reactivePresenter = reactivePresenter;
        
        _generalLevelConfigs = generalLevelConfigs;
    }
    
    public void CreateLevel()
    {
        _currentLevelItem = InstantiateLevel();
        
        _currentLevelItem.onLevelComplete += SendEventOnComplete;
        
        int currentLevelIndex = _currentLevelNumber % _levelsConfigs.LevelItems.Count;

        _currentLevelItem.Initialize
        (
            _generalLevelConfigs, _levelsConfigs.LevelItems[currentLevelIndex], 
            _elementsFactory,_reactivePresenter
            );
        
        onLevelItemSet?.Invoke(_currentLevelItem);
    }

    public void GoToNextLevel()
    {
        _reactivePresenter.ScoreSystem.ResetScore();

        _currentLevelNumber++;
        
        DestroyLevelIfNeeded();

        CreateLevel();
    }

    private void DestroyLevelIfNeeded()
    {
        if (IsCurrentLevelEmpty()) return;
        
        _currentLevelItem.onLevelComplete -= SendEventOnComplete;

        Destroy(_currentLevelItem.gameObject);

        _elementsFactory.ClearCells();
    }

    private bool IsCurrentLevelEmpty() => _currentLevelItem == null;
    private LevelItem InstantiateLevel() => Instantiate(_levelItemPrefab, transform);

    private void SendEventOnComplete(ELevelCompleteReason levelCompleteReason)
    {
        _currentLevelItem.Dispose();

        onLevelCompleted?.Invoke(levelCompleteReason);
    }
}
