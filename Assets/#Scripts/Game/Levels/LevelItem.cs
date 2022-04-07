using System;
using UnityEngine;

public class LevelItem : MonoBehaviour
{
    public event Action<ELevelCompleteReason> onLevelComplete = null;

    [SerializeField] private GameGridController _gameGridController = null;

    private void Awake()
    {
        _gameGridController.CreateAndInitializeGrid();
    }

    private void OnLevelCompleted(ELevelCompleteReason levelCompleteReason)
    {
        onLevelComplete?.Invoke(levelCompleteReason);
    }
}
