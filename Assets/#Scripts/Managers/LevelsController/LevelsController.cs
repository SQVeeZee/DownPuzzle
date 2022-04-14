using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsController : Singleton<LevelsController>
{
    public event Action<LevelItem> onLevelItemSet = null;
    public event Action<LevelItem> onLevelItemDispose = null;
    [SerializeField] private LevelItem _levelItemPrefab = null;
    
    [Header("Configs")]
    [SerializeField] private LevelsConfigs _levelsConfigs = null;

    private LevelItem _currentLevelItem = null;
    private int _currentLevelNumber = 0;

    public void CreateLevel()
    {
        InstantiateLevel();
        
        int currentLevelIndex = _currentLevelNumber % _levelsConfigs.LevelItems.Count;

        _currentLevelItem.SetLevelItemConfig(_levelsConfigs.LevelItems[currentLevelIndex]);
        
        _currentLevelItem.onLevelComplete += OnLevelComplete;

        UIManager.Instance.ShowScreen(EScreenType.GAME);

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
        if(!IsCurrentLevelEmpty())
        {
            _currentLevelItem.onLevelComplete -= OnLevelComplete;

            onLevelItemDispose?.Invoke(_currentLevelItem);

            Destroy(_currentLevelItem.gameObject);
        }
    }

    private bool IsCurrentLevelEmpty()
    {
        if(_currentLevelItem != null)
        {
            return false;
        }
        return true;
    }

    private void InstantiateLevel()
    {
        _currentLevelItem = Instantiate(_levelItemPrefab, transform);
    }

    private void OnLevelComplete(ELevelCompleteReason levelCompleteReason)
    {
        UIManager.Instance.ShowScreen(EScreenType.WIN);
    }
}
