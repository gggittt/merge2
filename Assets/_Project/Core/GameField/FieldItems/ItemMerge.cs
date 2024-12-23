using _Project.Core.GameField.Cells;
using UnityEngine;

namespace _Project.Core.GameField.FieldItems
{
[RequireComponent( typeof( ItemModel ), typeof( ItemMovement ) )]
public class ItemMerge : MonoBehaviour
{
    ItemMovement _movement;
    ItemModel _model;

    void Awake( )
    {
        _model = GetComponent<ItemModel>();
        _movement = GetComponent<ItemMovement>();
    }

    public void HandleDragTo( Cell draggedTo, Vector3 originalWorldPosition )
    {
        if ( draggedTo == null )
        {
            Debug.Log( $"<color=orange> no cell </color>" );
            RevertDragMove( originalWorldPosition );
        }
        else if ( draggedTo.HasItem && draggedTo.HoldedItem.CanBeMergedWith( _model ) )
        {
            Debug.Log( $"<color=green> {draggedTo} </color>, merge" );

            // merge
        }
        else if ( draggedTo.IsItemFrozen )
        {
            Debug.Log( $"<color=orange> {draggedTo} </color>, frozen" );

            RevertDragMove( originalWorldPosition );
            //add effects for user understand that cell is frozen: shake \ message
        }
        else if ( draggedTo.AvailableForReceiveItem )
        {
            Debug.Log( $"<color=green> {draggedTo} </color>, move to empty cell" );

            _movement.SetParentAndMoveToParent( draggedTo.transform );
        }
        else if ( draggedTo.HasItem )
        {
            Debug.Log( $"<color=lime> {draggedTo} </color>, swap" );
        }
        else
        {
            throw new System.Exception( "not implemented" );
        }
    }

    void RevertDragMove( Vector3 originalWorldPosition )
    {
        _movement.Move( originalWorldPosition );
    }
}
}