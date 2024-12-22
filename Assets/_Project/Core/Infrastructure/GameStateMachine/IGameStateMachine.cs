using _Project.Core.Infrastructure.GameStateMachine.States.FSNStatesContracts;

namespace _Project.Core.Infrastructure.GameStateMachine
{
public interface IGameStateMachine
{
    public void Enter<TState>( )
        where TState : IExcitableState;

    // public void Register( IExcitableState state ); //moved into StateMachine.Get<> for compliance OCP (no need to add new states in Bootstrapper)
}
}