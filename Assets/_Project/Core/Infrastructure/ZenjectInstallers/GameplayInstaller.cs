using _Project.Core.GameField;
using _Project.Core.GameField.Cells;
using _Project.Core.GameField.FieldItems;
using _Project.Core.Pool;
using _Project.Extensions.Zenject;
using UnityEngine;
using Zenject;

namespace _Project.Core.Infrastructure.ZenjectInstallers
{
public class GameplayInstaller : MonoInstaller
{
    [SerializeField] GameData _gameData;
    [SerializeField] CellCreator _cellCreator;
    [SerializeField] ItemModel _itemModelPrefab;


    public override void InstallBindings( )
    {
        Container.Bind<ICellGrid>()
           .To<CellGrid>()
           .AsSingle()
           .WithArguments( _gameData.BoardSize );

        Container.BindInterfacesAndSelfTo<ShapeTypes>()
           .AsSingle()
           .WithArguments( _gameData.ShapeTypes );

        Container.BindInterfacesAndSelfTo<MonoBehavioursObjectsPool<ItemModel>>()
           .AsSingle()
           .WithArguments( _itemModelPrefab, _gameData.BoardSize.x * _gameData.BoardSize.y );

        Container.BindInterfacesAndSelfTo<ItemFactory>()
           .AsSingle()
           // .WithArguments( _itemShapesDrawer )
           ;

        Container.BindInterfacesAndSelfAsSingleFromInstance( _gameData );
        Container.BindInterfacesAndSelfAsSingleFromInstance( _cellCreator );

    }
}
}