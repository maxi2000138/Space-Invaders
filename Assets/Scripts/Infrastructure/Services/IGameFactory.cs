using System;
using System.Collections.Generic;
using Enemy;
using UnityEngine;

namespace Infrastructure.Services
{
    public interface IGameFactory : IService
    {
        List<IProgressWatcher> ProgressWatchers { get; set; }
        List<IProgressReader> ProgressReaders { get; set; }
        void InstantiatePlayer();
        void InstantiateHUD();
        GameObject InstantiateEnemy();
        GameObject InstantiateBullet();

        void FindGameWorldSetup();
        Transform PlayerSpawnPoint { get; }
        GameObject[] EnemiesSpawns { get; }
        void InstantiateBulletPool();
        void InstantiateEnemyPool();
    }
}