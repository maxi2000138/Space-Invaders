using Infrastructure.Services;
using Infrastructure.StateMachine;
using Infrastructure.States;

namespace Infrastructure
{
    public class Game
    {
        private GameStateMachine _gameStateMachine;

        public Game()
        {
            _gameStateMachine = new GameStateMachine(AllServices.Container);
            _gameStateMachine.Enter<BootstrapState>();
        }
    }    
}
