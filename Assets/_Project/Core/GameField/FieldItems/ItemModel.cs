using _Project.Core.GameField.Cells;
using _Project.Core.Pool;
using UnityEngine;

namespace _Project.Core.GameField.FieldItems
{
[RequireComponent( typeof( ItemView ) )]
public class ItemModel : MonoBehaviour, IReleasable
{
    [field: SerializeField] public ShapeType Shape { get; private set; }
    [field: SerializeField] public int Level { get; private set; }

    public bool CanBeMatchedWith( ItemModel other ) => other && HasSameFlag( other );
    public bool HasSameFlag( ItemModel other ) => Shape.ContainsAny( other.Shape );

    public bool IsSameShape( ItemModel other ) => other && Shape == other.Shape;

    // public Cell ParentCell { get; private set; }

    public ItemView View { get; private set; }
    public ItemMovement Movement { get; private set; }

    public void Init( ShapeType shapeType, Vector3 position )
    {
        InitRequiredComponents();
        Shape = shapeType;

        transform.position = position;

        name = GetName();
    }

    public void SetParentAndMoveToParent( Cell parent )
    {
        // ParentCell = parent;
        transform.SetParent( parent.transform );
        transform.localPosition = Vector3.zero;
    }

    void OnValidate( )
    {
        InitRequiredComponents();
    }

    void InitRequiredComponents( )
    {
        if ( !View )
            View = GetComponent<ItemView>();
        if ( !Movement )
            Movement = GetComponent<ItemMovement>();
    }

    public override string ToString( ) => GetName();
    string GetName( ) => $"{GetType().Name}, {Shape}";

    void IReleasable.Release( )
    {
        View.PlayDestroyAnimation( OnAnimationFinish );

        void OnAnimationFinish( )
        {
            gameObject.SetActive( false );
            transform.parent = null;
        }
    }

}

}