using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Project.Core.Pool
{
public class MonoBehavioursObjectsPool<T>
    where T : MonoBehaviour, IReleasable
{
    readonly Queue<T> _objects;
    readonly T _prefab;

    public MonoBehavioursObjectsPool( T prefab, int prewarmObjectsAmount = 0 )
    {
        _prefab = prefab;
        _objects = new Queue<T>( prewarmObjectsAmount );

        for ( int i = 0; i < prewarmObjectsAmount; i++ )
        {
            T obj = Create( );
            obj.gameObject.SetActive( false );
        }
    }

    public T Get( )
    {
        T obj = _objects.FirstOrDefault( x => !x.isActiveAndEnabled );

        if ( obj == null )
            obj = Create();

        obj.gameObject.SetActive( true );
        return obj;
    }

    public void Release( T obj )
    {
        obj.Release();
    }

    T Create( )
    {
        T obj = Object.Instantiate( _prefab );
        _objects.Enqueue( obj );
        return obj;
    }
}

}
