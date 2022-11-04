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
        _gameFactory.InsantiateEnemies();
    }

    public void Exit()
    {
        
    }
}