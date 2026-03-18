using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    [SerializeField] private ZombieMover _enemyMover;
    [SerializeField] private LayerMask _targetLayerMask;
    [SerializeField] private Collider2D _detectorCollider;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!_detectorCollider.enabled)
            return;

        if (IsInTargetLayer(collision.gameObject))
        {
            _enemyMover.GoToPlayer(collision.transform);
            _detectorCollider.enabled = false;
        }
    }

    private bool IsInTargetLayer(GameObject obj)
    {
        return (_targetLayerMask.value & (1 << obj.layer)) > 0;
    }
}