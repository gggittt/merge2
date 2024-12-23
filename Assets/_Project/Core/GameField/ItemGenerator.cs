using System.Collections.Generic;
using _Project.Core.GameField.Cells;
using _Project.Extensions.Enumerable;
using UnityEngine;
using Zenject;

namespace _Project.Core.GameField
{
public class ItemGenerator : MonoBehaviour
{
    [Inject] ItemFactory _itemFactory;
    [Inject] ICellGrid _cellGrid;

    [Sirenix.OdinInspector.Button]
    public void CreateRandomItem( int amount = 1 )
    {
        HashSet<Cell> emptyCells = _cellGrid.GetFilteredItems( cell => cell.AvailableForReceiveItem );

        for ( int i = 0; i < amount; i++ )
        {
            if ( emptyCells.Count == 0 )
            {
                Debug.Log( "no empty cells" );
                return;
            }

            Cell randomEmptyCell = emptyCells.CutRandom();
            _itemFactory.CreateRandomItem( at: randomEmptyCell );
            Debug.Log( $"created at <color=cyan> {randomEmptyCell} </color>" );
        }
    }
}
}