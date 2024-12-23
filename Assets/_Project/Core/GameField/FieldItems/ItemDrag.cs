using _Project.Core.GameField.Cells;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.Core.GameField.FieldItems
{
[RequireComponent( typeof( ItemMerge ) )]
public class ItemDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Vector3 _originalWorldPosition;
    Vector3 _offset;
    Camera _camera;
    ItemMerge _merge;

    void Awake( )
    {
        _merge = GetComponent<ItemMerge>();
        _camera = Camera.main;
    }

    void IBeginDragHandler.OnBeginDrag( PointerEventData eventData )
    {
        Vector3 position = transform.position;
        _originalWorldPosition = position;
        _offset = position - _camera.ScreenToWorldPoint( eventData.position );
    }

    void IDragHandler.OnDrag( PointerEventData eventData )
    {
        Vector3 newPosition = _camera.ScreenToWorldPoint( eventData.position );
        newPosition.z = 0;
        transform.position = newPosition + _offset;
    }

    void IEndDragHandler.OnEndDrag( PointerEventData eventData )
    {
        Cell draggedTo = TryGetCellByRaycastAll2D();

        _merge.HandleDragTo( draggedTo, _originalWorldPosition );
    }

    Cell TryGetCellByRaycastAll2D( )
    {
        RaycastHit2D[] raycastHit2Ds = Physics2D.RaycastAll( transform.position, Vector2.zero, 9f );

        foreach ( RaycastHit2D hit in raycastHit2Ds )
        {
            if ( hit.collider.gameObject.TryGetComponent( out Cell cell ) )
                return cell;
        }

        return null;
    }
}

}