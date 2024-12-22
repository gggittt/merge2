using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.Core.GameField.FieldItems
{
public class ItemDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    Vector3 _offset;
    Camera _camera;

    // DropPlace _holder;

    void Awake( ) =>
        _camera = Camera.main;

    public void OnPointerClick( PointerEventData eventData )
    {
        Debug.Log( $"<color=cyan> {nameof( OnPointerClick )} </color>" );
    }

    void IBeginDragHandler.OnBeginDrag( PointerEventData eventData )
    {
        _offset = transform.position - _camera.ScreenToWorldPoint( eventData.position );

        Debug.Log( $"OnBeginDrag{eventData.pointerDrag.name} " );
    }

    void IDragHandler.OnDrag( PointerEventData eventData )
    {
        Vector3 newPosition = _camera.ScreenToWorldPoint( eventData.position );
        newPosition.z = 0;
        transform.position = newPosition + _offset;
    }

    void IEndDragHandler.OnEndDrag( PointerEventData eventData )
    {
        Debug.Log( $"OnEndDrag <color=cyan> {eventData.pointerDrag.name} </color>" );
    }
}
}