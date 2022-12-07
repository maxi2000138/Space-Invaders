using System;
using System.Collections.Generic;
using Enemy;
using Player;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Infrastructure.Services
{
    public class GameFactory : IGameFactory
    {
        public event Action OnAllEnemiesDye;
        public Transform PlayerSpawnPoint { get; private set; }
        public List<IProgressReader> ProgressReaders { get; set; } = new List<IProgressReader>();
        public List<IProgressWatcher> ProgressWatchers { get; set; } = new List<IProgressWatcher>();
        private readonly IEnemyStaticDataService _enemyStaticData;
        private readonly IScreenCharacteristicsService _screenCharacteristicsService;
        private readonly ISaveLoadService _saveLoadService;
        private ScoreCounter _scoreCounter;
        private Destroyer _destroyer;
        private List<GameObject> _enemies = new List<GameObject>();

        public GameFactory(IEnemyStaticDataService enemyStaticData,
            IScreenCharacteristicsService screenCharacteristicsService, ISaveLoadService saveLoadService)
        {
            _enemyStaticData = enemyStaticData;
            _screenCharacteristicsService = screenCharacteristicsService;
            _saveLoadService = saveLoadService;
            _screenCharacteristicsService.Construct();
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
            _scoreCounter = HUD.GetComponent<ScoreCounter>();
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
            enemy.transform.position = GetEnemyPosition(enemyData);
            _enemies.Add(enemy);
            return enemy;
        }

        private Vector3 GetEnemyPosition(EnemyData enemyData)
        {
            return new Vector3(_screenCharacteristicsService.StartPosition.x + enemyData._position.x * _screenCharacteristicsService.Width,
                _screenCharacteristicsService.StartPosition.y + enemyData._position.y * _screenCharacteristicsService.Height,
                _screenCharacteristicsService.StartPosition.z);
        }

        private void OnEnemyDie(EnemyBehaviour enemy)
        {
            enemy.OnEnemyDie -= OnEnemyDie;
            _destroyer.DestroyEnemy(enemy);
            _scoreCounter.Score++;
            _scoreCounter.ShowScore();
            if (_scoreCounter.CheckHighScore())
            {
                _saveLoadService.SaveProgress();
                _scoreCounter.ShowHighScore();
            }
            _enemies.Remove(enemy.gameObject);
            CheckAllEnemiesDie();
        }

        private void CheckAllEnemiesDie()
        {
            if(_enemies.Count == 0)
                OnAllEnemiesDye?.Invoke();
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