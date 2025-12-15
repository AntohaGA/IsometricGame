using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    [SerializeField] ZombieMover _enemyMover;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerGirl playerMover))
        {
            _enemyMover.GoToPlayer(playerMover);
        }
    }
}