using System;
using System.Collections.Generic;
using _Project.Core.Infrastructure.GameStateMachine.States.FSNStatesContracts;
using Zenject;

namespace _Project.Core.Infrastructure.GameStateMachine
{
public class StateMachineWithAutoInitStates : IGameStateMachine
{
    [Inject] IInstantiator _diContainer;

    readonly Dictionary<Type, IExcitableState> _states = new();

    IExcitableState _currentState;

    public void Enter<TState>( ) where TState : IExcitableState
    {
        TryExitPrevious();

        _currentState = Get<TState>();

        TryEnterNewState();
    }

    void TryExitPrevious( )
    {
        if ( _currentState is { } excitable )
            excitable.Exit();
    }

    void TryEnterNewState( )
    {
        if ( _currentState is IEnterableState enterable )
            enterable.Enter();
    }

    TState Get<TState>( ) where TState : IExcitableState
    {
        bool isStateAdded = _states.TryGetValue( typeof( TState ), out IExcitableState state );
        if ( isStateAdded )
            return (TState) state;

        TState createdState = _diContainer.Instantiate<TState>(); //CreateState<TState>();
        _states.Add( typeof( TState ), createdState ); //✅
        // _states.Add( createdState.GetType(), createdState ); //✅

        return createdState;
    }

    // TState CreateState<TState>( ) /*where TState : IExcitableState*/ =>
    //     _diContainer.Instantiate<TState>();
}
}