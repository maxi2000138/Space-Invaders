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
            InitGameWorld();
        }

        private void InitGameWorld()
        {
            _gameFactory.InstantiatePlayer();
            _gameFactory.InstantiateHUD();
            _gameFactory.InstantiateEnemies(OnEnemyDie);
        
            _gameStateMachine.Enter<GameLoopState>();
        }

        void OnEnemyDie(EnemyBehaviour enemyBehaviour)
        {
            Debug.Log("Kill");
            Object.Destroy(enemyBehaviour.gameObject);

        }

        public void Exit()
        {
        
        }
    }    
}
