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
    [SerializeField] MergeLevel _mergeLevel = new MergeLevel();
    public MergeLevel MergeLevel => _mergeLevel;

    public int Level => MergeLevel.Get();

    public bool CanBeMergedWith( ItemModel other )
    {
        bool hasSameLevelAndShape = other && HasSameLevel( other ) && HasSameShape( other );

        if ( hasSameLevelAndShape && HasNextLevel )
            return true;

        if ( hasSameLevelAndShape && HasNextLevel == false && other.HasNextLevel == false )
            Debug.Log( $"<color=yellow> bingo! Identical, both max level. Swap, no merge </color>" );

        return false;
    }

    public bool HasNextLevel => MergeLevel.IsMaxLevel == false;
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

    public void Release( )
    {
        _mergeLevel = new MergeLevel();
        Cell = null;

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
        cell.Item = this;
    }

    public void SetCellEmpty( )
    {
        Cell.Item = null;
    }

    public void PutItemInNew( Cell cell )
    {
        _movement.SetParentAndMoveToParent( cell.transform );
        SetCellEmpty();

        Cell = cell;
        cell.Item = this;
    }
}

}