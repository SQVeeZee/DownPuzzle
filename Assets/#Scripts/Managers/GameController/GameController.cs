using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private LevelsController _levelsController = null;
    
    void Awake()
    {
        _levelsController.CreateLevel();
    }
}
