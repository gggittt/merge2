using System;

namespace _Project.Core.GameField.FieldItems
{
[System.Serializable]
public class MergeLevel
{
    public event Action<int> OnValueChanged;
    int _value = 1;

    public int Get( ) => _value;

    public void Set( int value )
    {
        OnValueChanged?.Invoke( value );
        //add particles, congratulate player if he first reach a new level...
        _value = value;
    }
}
}