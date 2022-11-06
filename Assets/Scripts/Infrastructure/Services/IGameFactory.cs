using System;
using Enemy;
using UnityEngine;

namespace Infrastructure.Services
{
    public interface IGameFactory : IService
    {
        void InstantiatePlayer();
        void InstantiateHUD();
        void InstantiateEnemies(Action<EnemyBehaviour> onEnemyDie = null);
        GameObject InstantiateBullet(Vector3 spawnPosition);
    }
}