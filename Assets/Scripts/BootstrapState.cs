using Scenes;

public class BootstrapState : IState
{
    private readonly GameStateMachine _gameStateMachine;

    private AllServices _services;
    public BootstrapState(GameStateMachine gameStateMachine, AllServices services)
    {
        _gameStateMachine = gameStateMachine;
        _services = services;
        
        RegisterServices();
    }

    public void Enter()
    {
        _gameStateMachine.Enter<GameInitState>();
    }

    private void RegisterServices()
    {
        _services.RegisterServices<IGameFactory>(new GameFactory());
    }

    public void Exit()
    {
        
    }
}