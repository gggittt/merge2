using DG.Tweening;
using UnityEngine;

namespace _Project.Core.GameField.FieldItems
{
public class ItemMovement : MonoBehaviour
{
    [SerializeField] float _moveDuration = .4f;

    public void Move( Vector3 target )
    {
        transform.DOMove( target, duration: _moveDuration );
    }

    public void SetParentAndMoveToParent( Transform parent )
    {
        transform.SetParent( parent );
        // transform.localPosition = Vector3.zero;
        Move( parent.position );
    }

}
}