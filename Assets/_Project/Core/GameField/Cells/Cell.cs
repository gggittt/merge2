using System;
using _Project.Core.GameField.FieldItems;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.Core.GameField.Cells
{
[RequireComponent( typeof( SpriteRenderer ) )]
public class Cell : MonoBehaviour
{
    Vector2Int _localCoord;
    public Vector2Int LocalCoord => _localCoord;

    public ItemModel HoldedItem { get; set; }
    public bool HasItem => HoldedItem != null;
    public bool IsEmpty => HasItem == false;

    public void Init( Vector2Int coordinates )
    {
        _localCoord = coordinates;
        name = $"{nameof( Cell )} {_localCoord} ";
    }


}
}