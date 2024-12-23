using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Core.GameField.Cells
{
public interface ICellGrid : ICellGrid<Cell> { }
public interface ICellGrid<TCell>
{
    IEnumerable<TCell> Cells { get; }
    int Count { get; }
    int Width { get; }
    int Height { get; }

    HashSet<Vector2Int> GetCoordinatesOfFilteredItems(Func<TCell, bool> filter);
    HashSet<TCell> GetFilteredItems(Func<TCell, bool> filter);

    int GetIndexFromCoordinates( int x, int y );
    int GetIndexFromCoordinates( Vector2Int coordinates );
    Vector2Int GetCoordinatesFromIndex( int index );

    void Set( int x, int y, TCell value );
    void Set( Vector2Int coordinates, TCell value );
    void Set( int index, TCell value );
    TCell TryGet( int index );
    TCell TryGet( Vector2Int coordinates );

    bool IsXInBounds( int xIndex );
    bool IsYInBounds( int yIndex );
    bool IsInBounds( Vector2Int coordinates );
    Vector2Int GetCoords( TCell value );
}
}