using _Project.Core.GameField.FieldItems;
using _Project.Core.GameField.Shapes;
using UnityEngine;

namespace _Project.Core.GameField
{
public class ItemShapesDrawer : MonoBehaviour
{
    [SerializeField] SerializableDictionary<ShapeType, Sprite> _typeToSprite;

    public void SetSprite( ShapeType shape, ItemView itemView )
    {
        if ( _typeToSprite.TryGetValue( shape, out Sprite sprite ) )
        {
            itemView.SpriteRenderer.sprite = sprite;
        }
        else
        {
            Debug.LogError( $"<color=red> no sprite for shape {shape} </color>" );
        }
    }
}
}