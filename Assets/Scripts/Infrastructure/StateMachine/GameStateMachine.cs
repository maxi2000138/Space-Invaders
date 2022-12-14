using System;
using System.Collections.Generic;
using Infrastructure.Services;
using Infrastructure.States;

namespace Infrastructure.StateMachine
{
    public class GameStateMachine
    {
        private Dictionary<Type,IExitableState> _states;

        private IExitableState _curState;
        public GameStateMachine(AllServices services, ICoroutineRunner coroutineRunner)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, services, coroutineRunner),
                [typeof(LoadLevelState)] = new LoadLevelState(this, services.Single<ILoadLevelService>(),services.Single<IGameFactory>()),
                [typeof(GameInitState)] = new GameInitState(this, services.Single<IGameFactory>(), 
                    services.Single<IPersistantProgressService>(), services.Single<ISaveLoadService>()),
                [typeof(ChangeLevelState)] = new ChangeLevelState(this, services.Single<IGameFactory>(), services.Single<ILevelStaticDataService>()),
                [typeof(GameLoopState)] = new GameLoopState(this, services.Single<IGameFactory>()),
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            _curState?.Exit();
            TState state = GetState<TState>();
            _curState = state;
            state.Enter();
        }

        public void Enter<TState, TOverload>(TOverload overload) where TState : class,IOVerloadedState<TOverload>
        {
            _curState?.Exit();
            TState state = GetState<TState>();
            _curState = state;
            state.Enter(overload);
        }

        private TState GetState<TState>() where TState : class,IExitableState
        {
            return _states[typeof(TState)] as TState;
        }
    }
}
