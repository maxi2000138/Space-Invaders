using System;
using System.Collections.Generic;
using Enemy;
using UnityEngine;
using UnityEngine.InputSystem.Processors;

namespace Infrastructure.Services
{
    public class GameFactory : IGameFactory
    {
        public Transform PlayerSpawnPoint { get; private set; }
        public GameObject[] EnemiesSpawns { get; private set; }
        private BulletPool _bulletPool;
        private EnemyPool _enemyPool;
        private HighScoreCounter _scoreCounter;
        public List<IProgressReader> ProgressReaders { get; set; } = new List<IProgressReader>();
        public List<IProgressWatcher> ProgressWatchers { get; set; } = new List<IProgressWatcher>();

        public void FindGameWorldSetup()
        {
            PlayerSpawnPoint = GameObject.FindGameObjectWithTag(PrefabsPaths.PlayerSpawnPointTag).gameObject.transform;
            EnemiesSpawns = GameObject.FindGameObjectsWithTag(PrefabsPaths.EnemySpawnPointTag);
        }

        public void InstantiatePlayer() => 
            InstantiateRegistered(PlayerSpawnPoint.position, PrefabsPaths.PlayerPath);

        public void InstantiateHUD()
        {
            GameObject HUD = InstantiateRegistered(PrefabsPaths.HudPath);
            _scoreCounter = HUD.GetComponent<HighScoreCounter>();
        }

        public GameObject InstantiateBullet() => 
            InstantiateRegistered(PrefabsPaths.DefaultBulletPath);

        public void InstantiateBulletPool()
        {
            GameObject pool = InstantiateRegistered(PrefabsPaths.BulletPoolPath);
            _bulletPool = pool.GetComponent<BulletPool>();
        }

        public void InstantiateEnemyPool()
        {
            GameObject pool = InstantiateRegistered(PrefabsPaths.EnemyPoolPath);
            _enemyPool = pool.GetComponent<EnemyPool>();
        }


        public GameObject InstantiateEnemy()
        {
            object obj = InstantiateRegistered(PrefabsPaths.EnemyPath);

            if (obj is GameObject enemy)
            {
                enemy.GetComponent<EnemyBehaviour>().OnEnemyDie += OnEnemyDie;
            }
            else
            {
                throw new Exception("Error while creating enemy!");
            }

            return obj as GameObject;
        }

        private void OnEnemyDie(EnemyBehaviour enemy)
        {
            _enemyPool.Pool.TurnOffElement(enemy.gameObject);
            _scoreCounter._score++;
            AllServices.Container.Single<ISaveLoadService>().SaveProgress();
            _scoreCounter.ShowScore();
        }

        private GameObject InstantiateRegistered(Vector3 position, string path)
        {
            GameObject obj = UnityEngine.Object.Instantiate(Resources.Load(path), position, Quaternion.identity) as GameObject;
            IProgressReader[] readers = obj.GetComponentsInChildren<IProgressReader>();
            foreach (IProgressReader reader in readers)
            {
                ProgressReaders.Add(reader);
                
                if (reader is IProgressWatcher watcher)
                {
                    ProgressWatchers.Add(watcher);
                }
            }

            return obj;
        }

        public GameObject InstantiateRegistered(string path)
        {
            GameObject obj = UnityEngine.Object.Instantiate(Resources.Load(path)) as GameObject;
            IProgressReader[] readers = obj?.GetComponentsInChildren<IProgressReader>();
            if (readers != null)
            {
                foreach (IProgressReader reader in readers)
                {
                    ProgressReaders.Add(reader);
                    
                    if (reader is IProgressWatcher watcher)
                    {
                        ProgressWatchers.Add(watcher);
                    }
                }
            }

            return obj;
        }

    }
}