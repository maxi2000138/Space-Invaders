using Infrastructure.Services;
using Infrastructure.States;

namespace Infrastructure.StateMachine
{
    public class LoadLevelState : IOVerloadedState<string>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly ILoadLevelService _loadLevelService;

        public LoadLevelState(GameStateMachine gameStateMachine, ILoadLevelService loadLevelService, IGameFactory gameFactory)
        {
            _gameStateMachine = gameStateMachine;
            _loadLevelService = loadLevelService;
        }

        public void Exit()
        {
            
        }

        public void Enter(string overload)
        {
            _loadLevelService.LoadLevel(overload, OnLoadGame);
        }

        private void OnLoadGame()
        {
            _gameStateMachine.Enter<GameInitState>();
        }
    }
}