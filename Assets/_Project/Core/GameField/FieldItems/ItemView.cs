﻿using _Project.Core.UI;
using DG.Tweening;
using UnityEngine;

namespace _Project.Core.GameField.FieldItems
{
[RequireComponent( typeof( SpriteRenderer ) )]
public class ItemView : MonoBehaviour
{
    [SerializeField] CanvasForGo _canvasForGo;
    [SerializeField] float _disappearAnimationDuration = 0.3f;
    Vector3 _localScaleCashed;

    public SpriteRenderer SpriteRenderer { get; private set; }

    public void SetMergeLevel( int value )
    {
        _canvasForGo.Set( value );
        // SpriteRenderer.color = new Color(value, value, value);
    }

    void Awake( )
    {
        _localScaleCashed = transform.localScale;
        InitRequired();
    }

    void OnValidate( )
    {
        InitRequired();
    }

    void InitRequired( )
    {
        if ( !SpriteRenderer )
            SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void PlayDestroyAnimation( TweenCallback onComplete )
    {
        Sequence sequence = DOTween.Sequence()
           .SetEase( Ease.InOutSine );

        transform.DOScale( 0, _disappearAnimationDuration );

        sequence.OnComplete( onComplete );
    }

    public void PlayAppearAnimation( )
    {
        transform.DOScale( 0, 0 );
        transform.DOScale( _localScaleCashed, 0.2f );
    }

}
}