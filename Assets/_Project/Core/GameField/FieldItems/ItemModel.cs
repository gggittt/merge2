using _Project.Core.GameField.Cells;
using _Project.Core.GameField.Shapes;
using _Project.Core.Pool;
using UnityEngine;

namespace _Project.Core.GameField.FieldItems
{

[RequireComponent( typeof( ItemView ), typeof( ItemMovement ) )]
public class ItemModel : MonoBehaviour, IReleasable
{
    [field: SerializeField] public ShapeType Shape { get; private set; }
    [SerializeField] ItemMovement _movement;
    [SerializeField] ItemModel _nextLevelItem;

    public MergeLevel MergeLevel { get; } = new MergeLevel();
    public int Level => MergeLevel.Get();

    public bool CanBeMergedWith( ItemModel other ) => other && HasNextLevel && HasSameLevel( other ) && HasSameShape( other );
    public bool HasNextLevel => true; //_nextLevelItem != null; //mock
    public bool HasSameShape( ItemModel other ) => Shape.ContainsAny( other.Shape );
    public bool HasSameLevel( ItemModel other ) => Level == other.Level;

    public ItemView View { get; private set; }
    public Cell Cell;

    public void Init( ShapeType shapeType, Vector3 position )
    {
        InitRequiredComponents();
        Shape = shapeType;

        transform.position = position;

        MergeLevel.OnValueChanged += View.SetMergeLevel;
        View.SetMergeLevel( MergeLevel.Get() );

        name = ToString();
    }

    void OnValidate( )
    {
        InitRequiredComponents();
    }

    void InitRequiredComponents( )
    {
        if ( !View )
            View = GetComponent<ItemView>();
        if ( !_movement )
            _movement = GetComponent<ItemMovement>();
    }

    public override string ToString( ) => $"{GetType().Name}, {Shape}";

    void IReleasable.Release( )
    {
        View.PlayDestroyAnimation( OnAnimationFinish );

        void OnAnimationFinish( )
        {
            gameObject.SetActive( false );
            transform.parent = null;
        }
    }

    public void PutItemIn( Cell cell ) //todo refactor, cyclic dependency
    {
        _movement.SetParentAndMoveToParent( cell.transform );

        Cell = cell;
        cell.HoldedItem = this;
    }

    public void SetCellEmpty( )
    {
        Cell.HoldedItem = null;
    }

    public void PutItemInNew( Cell cell )
    {
        _movement.SetParentAndMoveToParent( cell.transform );
        SetCellEmpty();

        Cell = cell;
        cell.HoldedItem = this;
    }
}

}