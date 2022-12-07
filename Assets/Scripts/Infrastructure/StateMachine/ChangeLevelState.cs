using Infrastructure.Services;
using Infrastructure.States;

namespace Infrastructure.StateMachine
{
    public class ChangeLevelState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IGameFactory _gameFactory;
        private readonly ILevelStaticDataService _levelStaticDataService;
        private int levelNum = 0;
        
        public ChangeLevelState(GameStateMachine gameStateMachine, IGameFactory gameFactory,
            ILevelStaticDataService levelStaticDataService)
        {
            _gameStateMachine = gameStateMachine;
            _gameFactory = gameFactory;
            _levelStaticDataService = levelStaticDataService;
        }

        public void Enter()
        {
            
            _gameFactory.CreateEnemiesOnLevel(_levelStaticDataService.GiveLevel(levelNum++));
            _gameStateMachine.Enter<GameLoopState>();
        }

        public void Exit()
        {
            
        }
        
    }
}