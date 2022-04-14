using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private LevelsController _levelsController = null;
    
    void Start()
    {
        UIManager.Instance.DisableScreens();
        
        _levelsController.CreateLevel();
    }
}
