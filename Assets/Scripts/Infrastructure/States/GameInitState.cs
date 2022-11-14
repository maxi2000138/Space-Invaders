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

        public GameInitState(GameStateMachine gameStateMachine, IGameFactory gameFactory)
        {
            _gameStateMachine = gameStateMachine;
            _gameFactory = gameFactory;
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
            _gameFactory.InstantiateBulletPool();
            _gameFactory.InstantiateEnemyPool();
            _gameFactory.InstantiatePlayer();
            _gameFactory.InstantiateHUD();
            
            _gameStateMachine.Enter<GameLoopState>();
        }
    }    
}
