using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelItem : MonoBehaviour
{
    public event Action<ELevelCompleteReason> onLevelComplete = null;

    [SerializeField] private GameGridController _gameGridController = null;

    private void Awake()
    {
        _gameGridController.CreateFillGrid();
    }
}
