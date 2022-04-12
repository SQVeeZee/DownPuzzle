using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsController : Singleton<LevelsController>
{
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
    }

    private void GoToNextLevel()
    {
        _currentLevelNumber++;
        
        DestroyLevelIfNeeded();

        CreateLevel();
    }

    private void DestroyLevelIfNeeded()
    {
        if(!IsCurrentLevelEmpty())
        {
            Destroy(_currentLevelItem.gameObject);
        }
    }

    private bool IsCurrentLevelEmpty()
    {
        if(_currentLevelItem !=null)
        {
            return false;
        }
        return true;
    }

    private void InstantiateLevel()
    {
        _currentLevelItem = Instantiate(_levelItemPrefab, transform);
    }
}
