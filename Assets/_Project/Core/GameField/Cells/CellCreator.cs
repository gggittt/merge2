using UnityEngine;
using Zenject;

namespace _Project.Core.GameField.Cells
{
public class CellCreator : MonoBehaviour
{
    [SerializeField] Cell _cellPrefab;

    [Inject] GameData _gameData;
    [Inject] ICellGrid _cellGrid;
    // [Inject] SelectionManager _selectionManager;

    const int WorldToLocalCoefficient = - 1; //revertCreationTopToBotton, so first created cell will be on top, which easier to understand. // but cell will have negative world "Y" coordinate

    public void CreateBoard( )
    {
        Vector2Int gridSize = _gameData.BoardSize;
        Vector3 cellLocalScale = _cellPrefab.transform.localScale;

        for ( int y = 0; y < gridSize.y; y++ )
        for ( int x = 0; x < gridSize.x; x++ )
        {
            CreateTile( x, y, cellLocalScale );
        }
    }

    void CreateTile( int x, int y, Vector3 cellLocalScale )
    {
        Cell cell = Instantiate( _cellPrefab, transform );
        cell.Init(  new( x, y )  );
        _cellGrid.Set( x, y, cell );
        // cell.Clicked += _selectionManager.OnCellClick;

        SetCellPosition();
        void SetCellPosition( )
        {
            float xPos = x * cellLocalScale.x;
            float yPos = WorldToLocalCoefficient * y * cellLocalScale.y;
            Vector3 shiftFromPreviousCell = new(xPos, yPos);

            cell.transform.position = transform.position + shiftFromPreviousCell;
        }
    }
}
}