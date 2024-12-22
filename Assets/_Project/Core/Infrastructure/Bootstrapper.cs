using _Project.Core.Infrastructure.GameStateMachine;
using _Project.Core.Infrastructure.GameStateMachine.States;
using UnityEngine;
using Zenject;

namespace _Project.Core.Infrastructure
{
public class Bootstrapper : MonoBehaviour
{
    [Inject] IGameStateMachine _gameStateMachine;

    void Start( )
    {
        _gameStateMachine.Enter<BootstrapState>();

        KeepFsnInMemory();
    }

    void KeepFsnInMemory( ) =>
        DontDestroyOnLoad( this ); //or move FSM field to ProjectContext
}
}