﻿using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace _Project._Scripts._EditorTools.ToolbarButtons.Editor
{
public static class SceneHelper
{
    static string _sceneToOpen;

    public static void StartScene( string sceneName )
    {
        StopPlayIfPlaying();
        void StopPlayIfPlaying( )
        {
            if ( EditorApplication.isPlaying )
            {
                EditorApplication.isPlaying = false;
            }
        }

        _sceneToOpen = sceneName;
        EditorApplication.update += OnUpdate;
    }

    static void OnUpdate( )
    {
        if ( _sceneToOpen == null
             || EditorApplication.isPlaying
             || EditorApplication.isPaused
             || EditorApplication.isCompiling
             || EditorApplication.isPlayingOrWillChangePlaymode )
        {
            return;
        }

        EditorApplication.update -= OnUpdate;

        if ( EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo() )
        {
            // need to get scene via search because the path to the scene
            // file contains the package version so it'll change over time
            string[] guids = AssetDatabase.FindAssets( "t:scene " + _sceneToOpen, null );
            if ( guids.Length == 0 )
            {
                Debug.LogWarning( "Couldn't find scene file" );
            }
            else
            {
                string scenePath = AssetDatabase.GUIDToAssetPath( guids[ 0 ] );
                EditorSceneManager.OpenScene( scenePath );
                EditorApplication.isPlaying = true;
            }
        }

        _sceneToOpen = null;
    }
}
}