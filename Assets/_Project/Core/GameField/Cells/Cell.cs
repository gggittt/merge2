using System;
using _Project.Core.GameField.FieldItems;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.Core.GameField.Cells
{
[RequireComponent( typeof( SpriteRenderer ) )]
public class Cell : MonoBehaviour
{
    [field: SerializeField] public ItemModel Item { get; set; }
    [field: SerializeField] public bool IsItemFrozen { get; private set; }

    public Vector2Int LocalCoord { get; private set; }

    public bool HasItem => Item != null;
    public bool AvailableForReceiveItem => HasItem == false;

    public void Init( Vector2Int coordinates )
    {
        LocalCoord = coordinates;
        name = ToString();
    }

    public override string ToString( ) => $"{GetType().Name}, {LocalCoord}";
}
}