using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ZombieMover : MonoBehaviour
{
    [SerializeField] private ZombieAnimator _zombieAnimator;

    private NavMeshAgent _agent;
    private PlayerGirl _targetPlayer;

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

    public void GoToPlayer(PlayerGirl playerMover)
    {
        _targetPlayer = playerMover;
        _agent.enabled = true;
        _zombieAnimator.Run();
        _targetPlayer.Deceased += OnPlayerDied;
        StartCoroutine(UpdatePath());
    }

    private IEnumerator UpdatePath()
    {
        WaitForSeconds delay = new WaitForSeconds(0.2f);

        while (enabled)
        {
            _agent.SetDestination(_targetPlayer.transform.position);

            yield return delay;
        }

        Stop();
    }

    private void OnPlayerDied(PlayerGirl player)
    {
        Stop();
        _targetPlayer.Deceased -= OnPlayerDied;
        _targetPlayer  = null;
    }

    private void OnDisable()
    {
        if (_targetPlayer != null)
        {
            _targetPlayer.Deceased -= OnPlayerDied;
        }
    }
}