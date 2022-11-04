using System;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine
{
    private Dictionary<Type,IState> _states;

    private IState _curState;
    public GameStateMachine(AllServices services)
    {
        _states = new Dictionary<Type, IState>()
        {
            [typeof(BootstrapState)] = new BootstrapState(this, services),
            [typeof(GameInitState)] = new GameInitState(this, services.Single<IGameFactory>()),
            [typeof(GameLoopState)] = new GameLoopState(this),
        };
    }

    public void Enter<TState>() where TState : IState
    {
        _curState?.Exit();
        _curState = _states[typeof(TState)];
        _curState.Enter();
    }
}