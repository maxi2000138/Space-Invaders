using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyStaticDataService :  IEnemyStaticDataService
{
    private Dictionary<EnemyTypeId,EnemyStaticData> _enemies;

    public void LoadEnemies()
    {
        _enemies = Resources.LoadAll<EnemyStaticData>("StaticData/Enemies")
            .ToDictionary(x => x.TypeId, x => x);
    }

    public EnemyStaticData GiveEnemy(EnemyTypeId id) =>
        _enemies.TryGetValue(id, out EnemyStaticData enemy) 
            ? enemy 
            : null;
}
