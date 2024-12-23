﻿using _Project.Core.GameField.Cells;
using _Project.Core.GameField.FieldItems;
using _Project.Core.GameField.Shapes;
using _Project.Core.Pool;

namespace _Project.Core.GameField
{
public class ItemFactory
{
    readonly MonoBehavioursObjectsPool<ItemModel> _objectsPool;
    readonly ShapeTypes _shapeTypes;

    public ItemFactory( MonoBehavioursObjectsPool<ItemModel> objectsPool, ShapeTypes shapeTypes )
    {
        _objectsPool = objectsPool;
        _shapeTypes = shapeTypes;
    }

    public ItemModel CreateRandomItem( Cell cell )
    {
        ItemModel itemModel = _objectsPool.Get();
        ShapeType randomShape = _shapeTypes.RandomAllowedToSpawnType;

        itemModel.Init( randomShape, cell.transform.position );
        cell.HoldedItem = itemModel;
        itemModel.transform.parent = cell.transform;

        itemModel.View.PlayAppearAnimation();

        // _itemShapesDrawer.SetSprite( randomShape, itemModel.View.SpriteRenderer );

        return itemModel;
    }

}
}