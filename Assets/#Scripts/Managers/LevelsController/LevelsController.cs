using System;
using UnityEngine;

public class LevelsController : Singleton<LevelsController>
{
    public event Action<ELevelCompleteReason> onLevelCompleted = null;
    public event Action<LevelItem> onLevelItemSet = null;
    public event Action<LevelItem> onLevelItemDispose = null;
    
    [SerializeField] private LevelItem _levelItemPrefab = null;
    
    [Header("Configs")]
    [SerializeField] private LevelsConfigs _levelsConfigs = null;

    private GeneralLevelConfigs _generalLevelConfigs = null;
    
    private LevelItem _currentLevelItem = null;
    private int _currentLevelNumber = 0;

    public void Initialize(GeneralLevelConfigs generalLevelConfigs)
    {
        _generalLevelConfigs = generalLevelConfigs;
    }
    
    public void CreateLevel()
    {
        _currentLevelItem = InstantiateLevel();
        
        _currentLevelItem.onLevelComplete += SendEventOnComplete;
        
        int currentLevelIndex = _currentLevelNumber % _levelsConfigs.LevelItems.Count;

        _currentLevelItem.Initialize(_generalLevelConfigs, _levelsConfigs.LevelItems[currentLevelIndex]);
        
        onLevelItemSet?.Invoke(_currentLevelItem);
    }

    public void GoToNextLevel()
    {
        _currentLevelNumber++;
        
        DestroyLevelIfNeeded();

        CreateLevel();
    }

    private void DestroyLevelIfNeeded()
    {
        if (IsCurrentLevelEmpty()) return;
        
        _currentLevelItem.onLevelComplete -= SendEventOnComplete;

        _currentLevelItem.Dispose();
        
        onLevelItemDispose?.Invoke(_currentLevelItem);

        Destroy(_currentLevelItem.gameObject);
    }

    private bool IsCurrentLevelEmpty() => _currentLevelItem == null;
    private LevelItem InstantiateLevel() => Instantiate(_levelItemPrefab, transform);

    private void SendEventOnComplete(ELevelCompleteReason levelCompleteReason) => 
        onLevelCompleted?.Invoke(levelCompleteReason);
}
