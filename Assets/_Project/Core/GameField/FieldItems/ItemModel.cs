using _Project.Core.GameField.Cells;
using _Project.Core.GameField.Shapes;
using _Project.Core.Pool;
using UnityEngine;

namespace _Project.Core.GameField.FieldItems
{

[RequireComponent( typeof( ItemView ) )]
public class ItemModel : MonoBehaviour, IReleasable
{
    [SerializeField] ItemModel _nextLevelItem;
    [field: SerializeField] public ShapeType Shape { get; private set; }

    public int Level => MergeLevel.Get();
    // [field: SerializeField, Min( 1 )] public int Level { get; private set; } = 1;

    [SerializeField] public MergeLevel MergeLevel;

    public bool CanBeMergedWith( ItemModel other ) => other && HasNextLevel && HasSameLevel( other ) && HasSameShape( other );
    public bool HasNextLevel => _nextLevelItem != null;
    public bool HasSameShape( ItemModel other ) => Shape.ContainsAny( other.Shape );
    public bool HasSameLevel( ItemModel other ) => Level == other.Level;

    public ItemView View { get; private set; }

    public void Init( ShapeType shapeType, Vector3 position )
    {
        InitRequiredComponents();
        Shape = shapeType;

        transform.position = position;

        MergeLevel.OnValueChanged += View.SetMergeLevel;

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

}

}