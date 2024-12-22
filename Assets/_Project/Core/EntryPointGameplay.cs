using _Project.Core.GameField;
using _Project.Core.GameField.Cells;
using UnityEngine;
using Zenject;

namespace _Project.Core
{
public class EntryPointGameplay : MonoBehaviour
{
    [Inject] CellCreator _cellCreator;
    [Inject] ItemFactory _itemFactory;
    [Inject] ICellGrid _cellGrid;

    void Start( )
    {
        _cellCreator.CreateBoard();

        _itemFactory.CreateItem( _cellGrid.TryGet( 4 ) );
        _itemFactory.CreateItem( _cellGrid.TryGet( 8 ) );
        _itemFactory.CreateItem( _cellGrid.TryGet( 15 ) );
        _itemFactory.CreateItem( _cellGrid.TryGet( 16 ) );
    }

}
}