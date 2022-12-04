using System;
using System.Collections.Generic;
using Enemy;
using UnityEngine;
using UnityEngine.InputSystem.Processors;
using Object = UnityEngine.Object;

namespace Infrastructure.Services
{
    public class GameFactory : IGameFactory
    {
        public Transform PlayerSpawnPoint { get; private set; }
        public List<IProgressReader> ProgressReaders { get; set; } = new List<IProgressReader>();
        public List<IProgressWatcher> ProgressWatchers { get; set; } = new List<IProgressWatcher>();
        private BulletPool _bulletPool;
        private IEnemyStaticDataService _staticData;
        private EnemyPool _enemyPool;
        private HighScoreCounter _scoreCounter;

        public GameFactory(IEnemyStaticDataService staticData)
        {
            _staticData = staticData;
        }
        
        public void FindGameWorldSetup()
        {
            PlayerSpawnPoint = GameObject.FindGameObjectWithTag(PrefabsPaths.PlayerSpawnPointTag).gameObject.transform;
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

        public void CreateEnemiesOnLevel(LevelStaticData staticData)
        {
            foreach (EnemyData enemy in staticData.Enemies)
            {
                InstantiateEnemy(enemy);
            }
        }

        private GameObject InstantiateEnemy(EnemyData enemyData)
        {
            EnemyStaticData staticData = GameObject.Instantiate(_staticData.GiveEnemy(enemyData._typeId));
            GameObject enemy = Object.Instantiate(staticData.Prefab);
            enemy.GetComponent<EnemyBehaviour>().OnEnemyDie += OnEnemyDie;
            enemy.transform.position = enemyData._position;
            return enemy;
        }

        private void OnEnemyDie(EnemyBehaviour enemy)
        {
            enemy.OnEnemyDie -= OnEnemyDie;
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