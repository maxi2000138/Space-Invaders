using UnityEngine;

public interface IGameFactory : IService
{
    void InstantiatePlayer();
    void InstantiateHUD();
    void InsantiateEnemies();
    void InstantiateBullet(Vector3 spawnPosition);
}