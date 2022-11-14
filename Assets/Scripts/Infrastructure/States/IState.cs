using System;

public interface IState : IExitableState
{
    void Enter();


}
public interface IOVerloadedState<TOverload> : IExitableState
{
    void Enter(TOverload overload);
    
}

public interface IExitableState
{
    void Exit();
}