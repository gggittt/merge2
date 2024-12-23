using System;

namespace _Project.Core.GameField.FieldItems
{
[System.Serializable]
public class MergeLevel
{
    public event Action<int> OnValueChanged;
    int _value = 1;

    public int Get( ) => _value;

    public int Set( int value )
    {
        OnValueChanged?.Invoke( value );
        return _value = value;
    }
}
}