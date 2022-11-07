using System;
using Bullet;
using Enemy;
using Player;
using UnityEngine;

namespace Infrastructure.Services
{
    public class GameFactory : IGameFactory
    {
        private readonly Transform _playerSpawnPoint;
        private readonly GameObject[] _enemiesSpawns;
        private IGameFactory _gameFactoryImplementation;


        public GameFactory()
        {
            _playerSpawnPoint = GameObject.FindGameObjectWithTag(PrefabsPaths.PlayerSpawnPointTag).gameObject.transform;
            _enemiesSpawns = GameObject.FindGameObjectsWithTag(PrefabsPaths.EnemySpawnPointTag);
        }

        public void InstantiatePlayer() => 
            Instantiate(_playerSpawnPoint.position, PrefabsPaths.PlayerPath);

        public void InstantiateHUD() => 
            Instantiate(PrefabsPaths.HudPath);

        public void InstantiateEnemies(Action<EnemyBehaviour> enemyDied = null)
        {
            foreach (GameObject spawnPoint in _enemiesSpawns)
            {
                object obj = Instantiate(spawnPoint.transform.position, PrefabsPaths.EnemyPath);
                if (obj is GameObject enemy)
                {
                    enemy.GetComponent<EnemyBehaviour>().OnEnemyDie += enemyDied;
                }
                else
                {
                    Debug.Log("Error while creating enemy!");
                }
            }
        }

        public GameObject InstantiateBullet() => 
            Instantiate(PrefabsPaths.DefaultBulletPath);

        private GameObject Instantiate(Vector3 position, string path) => 
            UnityEngine.Object.Instantiate(Resources.Load(path), position, Quaternion.identity) as GameObject;

        public GameObject Instantiate(string path) => 
            UnityEngine.Object.Instantiate(Resources.Load(path)) as GameObject;
    }
}