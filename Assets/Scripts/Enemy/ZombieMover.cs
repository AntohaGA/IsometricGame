using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ZombieMover : MonoBehaviour
{
    [SerializeField] private ZombieAnimator _zombieAnimator;

    private NavMeshAgent _agent;
    private Transform _targetPlayer;
    private Coroutine _pathUpdateCoroutine;
    private readonly WaitForSeconds _pathUpdateDelay = new WaitForSeconds(0.3f);

    private void OnEnable()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _agent.enabled = false;
    }    

    public void GoToPlayer(Transform player)
    {
        _targetPlayer = player;
        _agent.enabled = true;
        _zombieAnimator.Run();

        if (_pathUpdateCoroutine != null)
        {
            StopCoroutine(_pathUpdateCoroutine);
        }

        _pathUpdateCoroutine = StartCoroutine(UpdatePath());
    }

    public void Stop()
    {
        _agent.enabled = false;

        if (_pathUpdateCoroutine != null)
        {
            StopCoroutine(_pathUpdateCoroutine);
            _pathUpdateCoroutine = null;
        }
    }

    private IEnumerator UpdatePath()
    {
        while (enabled && _targetPlayer != null)
        {
            _agent.SetDestination(_targetPlayer.position);

            yield return _pathUpdateDelay;
        }

        Stop();
    }
}