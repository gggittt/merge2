using TMPro;
using UnityEngine;

namespace _Project.Core.UI
{
public class CanvasForGo : MonoBehaviour
{
    [SerializeField] TMP_Text _text;

    public void Set( string value )
    {
        _text.text = value;
    }

    public void Set( int value )
    {
        _text.text = value.ToString();
    }
}
}