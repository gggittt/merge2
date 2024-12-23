using _Project.Core.GameField.Cells;
using _Project.Extensions.UnityTypes;
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

    public void HandleDragTo( Cell target, Vector3 originalWorldPosition )
    {
        if ( target == null )
        {
            Debug.Log( $"<color=orange> no cell </color>" );
            RevertDragMove( originalWorldPosition );
        }
        else if ( _model.Cell == target )
        {
            Debug.Log( $"<color=orange> {target} </color>, same cell" );
            RevertDragMove( originalWorldPosition );
        }
        else if ( target.HasItem && target.Item.CanBeMergedWith( _model ) )
        {
            Debug.Log( $"<color=green> {target} </color>, merge" );

            MergeThisWithItemIn( target );
        }
        else if ( target.IsItemFrozen )
        {
            Debug.Log( $"<color=orange> {target} </color>, frozen" );

            RevertDragMove( originalWorldPosition );
            //add effects for user understand that cell is frozen: shake \ UI message
        }
        else if ( target.AvailableForReceiveItem )
        {
            Debug.Log( $"<color=green> {target} </color>, move to empty cell" );

            _model.PutItemInNew( target );
        }
        else if ( target.HasItem )
        {
            Debug.Log( $"<color=lime> {target} </color>, swap" );

            SwapItems( target );
        }
        else
        {
            throw new System.Exception( "not implemented" );
        }
    }

    void MergeThisWithItemIn( Cell target )
    {
        target.Item.MergeLevel.Set( _model.Level + 1 );
        //should be: destroy both current items, and create a new one from "ItemModel_nextLevelItem", but I will leave it like that for now
        _model.SetCellEmpty();
        _model.Release();
    }

    void SwapItems( Cell target )
    {
        ItemModel itemInTargetCell = target.Item;
        Cell cellWhereDragStarted = _model.Cell;

        _model.PutItemIn( target );
        itemInTargetCell.PutItemIn( cellWhereDragStarted );
    }

    void RevertDragMove( Vector3 originalWorldPosition )
    {
        _movement.Move( originalWorldPosition );
    }
}
}