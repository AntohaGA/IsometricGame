using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ZombieMover : MonoBehaviour
{
    [SerializeField] private ZombieAnimator _zombieAnimator;

    private NavMeshAgent _agent;

    private void OnEnable()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _agent.enabled = false;
    }

    public void Stop()
    {
        _agent.enabled = false;
    }

    public void GoToPlayer(PlayerGirl playerMover)
    {
        _agent.enabled = true;
        _zombieAnimator.Run();
        StartCoroutine(UpdatePath(playerMover));
    }

    private IEnumerator UpdatePath(PlayerGirl playerMover)
    {
        WaitForSeconds delay = new WaitForSeconds(0.2f);

        while (enabled)
        {
            _agent.SetDestination(playerMover.transform.position);

            yield return delay;
        }
    }
}