using System;
using UnityEngine;

namespace _Project.Core.GameField.FieldItems
{
[System.Serializable]
public class MergeLevel
{
    [SerializeField, Min( 1 )] int _value = 1;
    [SerializeField, Min( 1 )] int _maxLevel = 4;

    public event Action<int> OnValueChanged;

    public bool IsMaxLevel => _value == _maxLevel;

    public int Get( ) => _value;

    public void Set( int value )
    {
        OnValueChanged?.Invoke( value );
        //add particles, congratulate player if he open a new level first time
        _value = value;
    }
}
}