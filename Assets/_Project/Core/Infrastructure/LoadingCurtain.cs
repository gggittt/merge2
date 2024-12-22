using System.Collections;
using UnityEngine;

namespace _Project.Core.Infrastructure
{
public class LoadingCurtain : MonoBehaviour
{
    [SerializeField] CanvasGroup Curtain;
    [SerializeField] float _alphaChangeStep = 0.03f;
    [SerializeField] float _changeSpeed = 0.3f;

    void Awake( )
    {
        DontDestroyOnLoad( this );
    }

    public void Show( )
    {
        gameObject.SetActive( true );
        Curtain.alpha = 1;
    }

    public void Hide( ) =>
        StartCoroutine( DoFadeIn() );

    IEnumerator DoFadeIn( )
    {
        while ( Curtain.alpha > 0 )
        {

            Curtain.alpha -= _alphaChangeStep;
            yield return new WaitForSeconds( _alphaChangeStep * _changeSpeed );
        }

        gameObject.SetActive( false );
    }
}
}