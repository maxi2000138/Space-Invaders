using System;
using Enemy;
using UnityEngine;

namespace Infrastructure.Services
{
    public class GameFactory : IGameFactory
    {
        public Transform PlayerSpawnPoint { get; private set; }
        public GameObject[] EnemiesSpawns { get; private set; }

        private BulletPool _bulletPool;

        private EnemyPool _enemyPool;

        public void FindGameWorldSetup()
        {
            PlayerSpawnPoint = GameObject.FindGameObjectWithTag(PrefabsPaths.PlayerSpawnPointTag).gameObject.transform;
            EnemiesSpawns = GameObject.FindGameObjectsWithTag(PrefabsPaths.EnemySpawnPointTag);
        }

        public void InstantiatePlayer() => 
            Instantiate(PlayerSpawnPoint.position, PrefabsPaths.PlayerPath);

        public void InstantiateHUD() => 
            Instantiate(PrefabsPaths.HudPath);

        public GameObject InstantiateBullet() => 
            Instantiate(PrefabsPaths.DefaultBulletPath);

        public void InstantiateBulletPool()
        {
            GameObject pool = Instantiate(PrefabsPaths.BulletPoolPath);
            _bulletPool = pool.GetComponent<BulletPool>();
        }

        public void InstantiateEnemyPool()
        {
            GameObject pool = Instantiate(PrefabsPaths.EnemyPoolPath);
            _enemyPool = pool.GetComponent<EnemyPool>();
        }


        public GameObject InstantiateEnemy()
        {
            object obj = Instantiate(PrefabsPaths.EnemyPath);
            
            if (obj is GameObject enemy)
            {
                enemy.GetComponent<EnemyBehaviour>().OnEnemyDie += OnEnemyDie;
            }
            else
            {
                throw  new Exception("Error while creating enemy!");
            }
            
            return obj as GameObject;
        }
        
        private void OnEnemyDie(EnemyBehaviour enemy) => 
            _enemyPool.Pool.TurnOffElement(enemy.gameObject);

        private GameObject Instantiate(Vector3 position, string path) => 
            UnityEngine.Object.Instantiate(Resources.Load(path), position, Quaternion.identity) as GameObject;

        public GameObject Instantiate(string path) => 
            UnityEngine.Object.Instantiate(Resources.Load(path)) as GameObject;
    }
}