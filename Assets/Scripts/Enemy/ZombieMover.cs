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

    public void Stop()
    {
        _agent.enabled = false;
        _zombieAnimator.Stand();

        if (_pathUpdateCoroutine != null)
        {
            StopCoroutine(_pathUpdateCoroutine);
            _pathUpdateCoroutine = null;
        }

        if (_targetPlayer != null)
        {
            Health playerHealth = _targetPlayer.GetComponent<Health>();

            if (playerHealth != null)
            {
                playerHealth.Destroyd -= HandlePlayerDeath;
            }
        }

        _targetPlayer = null;
    }

    public void GoToPlayer(Transform player)
    {
        if (player == null) return;

        _targetPlayer = player;
        _agent.enabled = true;

        if (_zombieAnimator != null)
        {
            _zombieAnimator.Run();
        }

        if (_pathUpdateCoroutine != null)
        {
            StopCoroutine(_pathUpdateCoroutine);
        }

        _pathUpdateCoroutine = StartCoroutine(UpdatePath());

        Health playerHealth = _targetPlayer.GetComponent<Health>();

        if (playerHealth != null)
        {
            playerHealth.Destroyd += HandlePlayerDeath;
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

    private void HandlePlayerDeath()
    {
        Debug.Log("Игрок умер! Зомби останавливается.");
        Stop(); // Вызываем метод Stop для полной очистки
    }
}