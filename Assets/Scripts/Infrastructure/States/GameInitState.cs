using Enemy;
using Infrastructure.Services;
using Infrastructure.StateMachine;
using UnityEngine;

namespace Infrastructure.States
{
    public class GameInitState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistantProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;
        private readonly ILevelStaticDataService _levelStaticDataService;

        public GameInitState(GameStateMachine gameStateMachine, IGameFactory gameFactory,
            IPersistantProgressService progressService, ISaveLoadService saveLoadService)
        {
            _gameStateMachine = gameStateMachine;
            _gameFactory = gameFactory;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            _gameFactory.FindGameWorldSetup();
            InitGameWorld();
        }

        public void Exit()
        {
        
        }

        private void InitGameWorld()
        {
            StopGame();
            _gameFactory.InstantiateHUD();
            _gameFactory.InstantiateBulletPool();
            _gameFactory.InstantiateEnemyPool();
            _gameFactory.InstantiatePlayer();
            
            LoadProgressOrInit();
            
            _gameStateMachine.Enter<ChangeLevelState>();
        }

        public void StopGame()
        {
            Time.timeScale = 0f;
            //OwnInput.PlayerActions
        }
        

        private void LoadProgressOrInit()
        {
             _progressService.PlayerProgress = _saveLoadService.LoadProgress() ?? new PlayerProgress();
             foreach (IProgressReader reader in _gameFactory.ProgressReaders)
             {
                 reader.LoadProgress(_progressService.PlayerProgress);
             }
        }
    }    
}
