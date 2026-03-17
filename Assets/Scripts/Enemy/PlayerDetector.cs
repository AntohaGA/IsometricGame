using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    [SerializeField] private ZombieMover _enemyMover;
    [SerializeField] private LayerMask _targetLayerMask;

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("OnTriggerStay2D");

        if (IsInTargetLayer(collision.gameObject))
        {
            _enemyMover.GoToPlayer(collision.transform);
        }
    }

    private bool IsInTargetLayer(GameObject obj)
    {
        return (_targetLayerMask.value & (1 << obj.layer)) > 0;
    }
}