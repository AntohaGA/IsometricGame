using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ZombieMover : MonoBehaviour
{
    [SerializeField] private ZombieAnimator _zombieAnimator;

    private NavMeshAgent _agent;
    private Transform _targetPlayer;

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
        _zombieAnimator.Stand(); // останавливаем анимацию
    }

    public void GoToPlayer(Transform player)
    {
        _targetPlayer = player;
        _agent.enabled = true;
        _zombieAnimator.Run();
        StartCoroutine(UpdatePath());
    }

    private IEnumerator UpdatePath()
    {
        WaitForSeconds delay = new WaitForSeconds(0.3f);

        while (enabled)
        {
            _agent.SetDestination(_targetPlayer.transform.position);

            yield return delay;
        }

        Stop();
    }
}