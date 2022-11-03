using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBootstraper : MonoBehaviour
{
    private Game _game;

    private void Awake()
    {
        _game = new Game();

        DontDestroyOnLoad(this);
    }
    
    
}