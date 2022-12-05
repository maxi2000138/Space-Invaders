using System;
using System.Collections.Generic;
using Enemy;
using Player;
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
        private IEnemyStaticDataService _enemyStaticData;
        private ILevelStaticDataService _levelStaticDataService;
        private HighScoreCounter _scoreCounter;
        private Destroyer _destroyer;
        public GameFactory(IEnemyStaticDataService enemyStaticData, ILevelStaticDataService levelStaticDataService)
        {
            _enemyStaticData = enemyStaticData;
            _levelStaticDataService = levelStaticDataService;
        }
        
        public void FindGameWorldSetup()
        {
            PlayerSpawnPoint = GameObject.FindGameObjectWithTag(PrefabsPaths.PlayerSpawnPointTag).gameObject.transform;
        }

        public void InstantiatePlayer()
        {
            GameObject go = InstantiateRegistered(PlayerSpawnPoint.position, PrefabsPaths.PlayerPath);
            go.GetComponent<PlayerShoot>().Construct(this);
        }

        public void InstantiateDestroyer() => 
            _destroyer = InstantiateRegistered(PrefabsPaths.DestroyerPath).GetComponent<Destroyer>();

        public void InstantiateHUD()
        {
            GameObject HUD = InstantiateRegistered(PrefabsPaths.HudPath);
            _scoreCounter = HUD.GetComponent<HighScoreCounter>();
        }
        
        public GameObject InstantiateBullet() => 
            InstantiateRegistered(PrefabsPaths.DefaultBulletPath);
        
        public void CreateEnemiesOnLevel(LevelStaticData staticData)
        {
            foreach (EnemyData enemy in staticData.Enemies)
            {
                InstantiateEnemy(enemy);
            }
        }

        private GameObject InstantiateEnemy(EnemyData enemyData)
        {
            EnemyStaticData staticData = GameObject.Instantiate(_enemyStaticData.GiveEnemy(enemyData._typeId));
            GameObject enemy = Object.Instantiate(staticData.Prefab);
            enemy.GetComponent<EnemyBehaviour>().OnEnemyDie += OnEnemyDie;
            enemy.transform.position = enemyData._position;
            return enemy;
        }

        private void OnEnemyDie(EnemyBehaviour enemy)
        {
            enemy.OnEnemyDie -= OnEnemyDie;
            _destroyer.DestroyEnemy(enemy);
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