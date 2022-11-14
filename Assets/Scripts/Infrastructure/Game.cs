using Infrastructure.Services;
using Infrastructure.StateMachine;
using Infrastructure.States;

namespace Infrastructure
{
    public class Game
    {
        private GameStateMachine _gameStateMachine;

        public Game(ICoroutineRunner coroutineRunner)
        {
            _gameStateMachine = new GameStateMachine(AllServices.Container, coroutineRunner);
            _gameStateMachine.Enter<BootstrapState>();
        }
    }    
}
