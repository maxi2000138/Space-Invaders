using System;
using Enemy;
using UnityEngine;

namespace Infrastructure.Services
{
    public interface IGameFactory : IService
    {
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