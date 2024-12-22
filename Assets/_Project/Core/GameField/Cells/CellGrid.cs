using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Core.GameField.Cells
{
public class CellGrid : ICellGrid
{
    readonly Cell[] _cells;
    public IEnumerable<Cell> Cells => _cells;

    public int Count => _cells.Length;
    public int Width { get; }
    public int Height { get; }

    public CellGrid( int width, int height )
    {
        _cells = new Cell[width * height];
        Width = width;
        Height = height;
    }

    public CellGrid( Vector2Int size ) : this( size.x, size.y ) { }

    public HashSet<Vector2Int> GetCoordinatesOfFilteredItems( Func<Cell, bool> filterForEachItem )
    {
        var result = new HashSet<Vector2Int>();

        for ( int x = 0; x < Width; x++ )
        for ( int y = 0; y < Height; y++ )
        {
            Cell cell = _cells[ GetIndexFromCoordinates( x, y ) ];
            if ( filterForEachItem( cell ) )
            {
                result.Add( new Vector2Int( x, y ) );
            }
        }

        return result;
    }

    public int GetIndexFromCoordinates( int x, int y ) =>
        y * Width + x;

    public int GetIndexFromCoordinates( Vector2Int coordinates ) =>
        GetIndexFromCoordinates( coordinates.x, coordinates.y );

    public Vector2Int GetCoordinatesFromIndex( int index ) =>
        new Vector2Int( index % Width, index / Width );

    public void Set( int x, int y, Cell value ) =>
        _cells[ GetIndexFromCoordinates( x, y ) ] = value;

    public void Set( Vector2Int coordinates, Cell value ) =>
        Set( coordinates.x, coordinates.y, value );

    public void Set( int index, Cell value ) =>
        _cells[ index ] = value;

    public Cell TryGet( int index )
    {
        if ( index <= Count )
        {
            return Get( index );
        }

        throw new IndexOutOfRangeException();
    }

    public Cell TryGet( Vector2Int coordinates )
    {
        if ( IsXInBounds( coordinates.x ) == false )
        {
            return default;
        }

        if ( IsYInBounds( coordinates.y ) == false )
        {
            return default;
        }

        int index = GetIndexFromCoordinates( coordinates.x, coordinates.y );
        return Get( index );
    }

    Cell Get( int index )
    {
        Cell cell;
        try
        {
            cell = _cells[ index ];

        } catch (Exception e)
        {
            Debug.Log( e );
            throw;
        }

        return cell;
    }

    public bool IsXInBounds( int xIndex ) =>
        ( xIndex >= 0 && xIndex < Width );

    public bool IsYInBounds( int yIndex ) =>
        ( yIndex >= 0 && yIndex < Height );

    public bool IsInBounds( Vector2Int coordinates ) =>
        IsXInBounds( coordinates.x ) && IsYInBounds( coordinates.y );

    public Vector2Int GetCoords( Cell value )
    {
        int i = Array.IndexOf( _cells, value );
        if ( i == - 1 )
        {
            throw new ArgumentException( "cell not found in grid" );
        }

        return GetCoordinatesFromIndex( i );
    }

}
}