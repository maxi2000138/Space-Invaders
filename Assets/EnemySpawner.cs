using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]private int _amountEnemiesByRow;
    private float _sizeEnemyByRow;
    

    private void Start()
    {
        _sizeEnemyByRow = (PlayingFieldBorders.RightBoundX - PlayingFieldBorders.LeftBoundX) / _amountEnemiesByRow;
        
    }
}
