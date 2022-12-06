using Infrastructure.Services;
using Infrastructure.StateMachine;

namespace Infrastructure.States
{
    public class GameLoopState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IGameFactory _gameFactory;

        public GameLoopState(GameStateMachine gameStateMachine, IGameFactory gameFactory)
        {
            _gameStateMachine = gameStateMachine;
            _gameFactory = gameFactory;
            gameFactory.OnAllEnemiesDye += ChangeLevel;
        }

        private void ChangeLevel()
        {
            _gameStateMachine.Enter<ChangeLevelState>();
        }

        public void Enter()
        {
          
        }

        public void Exit()
        {
            _gameFactory.OnAllEnemiesDye -= ChangeLevel; 
        }
    }    
}
