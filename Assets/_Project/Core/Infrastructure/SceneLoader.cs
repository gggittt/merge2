using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Core.Infrastructure
{
public class SceneLoader
{
    readonly ICoroutineRunner _coroutineRunner;

    public SceneLoader( ICoroutineRunner coroutineRunner ) =>
        _coroutineRunner = coroutineRunner;

    public void Load( string sceneName, Action onLoaded = null ) =>
        _coroutineRunner.StartCoroutine( LoadScene( sceneName, onLoaded ) );

    IEnumerator LoadScene( string nextScene, Action onLoaded )
    {
        if ( SceneManager.GetActiveScene().name == nextScene )
        {
            onLoaded?.Invoke();
            yield break;
        }

        AsyncOperation waitNextScene = SceneManager.LoadSceneAsync( nextScene );

        while ( waitNextScene is { isDone: false } ) //ReplaceNullReferenceException
            yield return null;

        InvokeActionIfNeeded();
        void InvokeActionIfNeeded( )
        {
            bool successfullyLoaded = waitNextScene is { };
            if ( successfullyLoaded )
                onLoaded?.Invoke();
            else
                Debug.LogError( $"probably, not added to build settings: {nextScene}" );
        }
    }
}
}