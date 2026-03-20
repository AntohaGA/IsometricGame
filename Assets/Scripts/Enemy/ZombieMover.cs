using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ZombieMover : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Coroutine _pathUpdateCoroutine;
    private readonly WaitForSeconds _pathUpdateDelay = new WaitForSeconds(0.3f);

    public event Action OnRun;

    private void OnEnable()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _agent.enabled = false;
    }    

    public void GoTo(Transform target)
    {
        _agent.enabled = true;
        OnRun?.Invoke();

        if (_pathUpdateCoroutine != null)
        {
            StopCoroutine(_pathUpdateCoroutine);
        }

        _pathUpdateCoroutine = StartCoroutine(UpdatePath(target));
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

    private IEnumerator UpdatePath(Transform target)
    {
        while (enabled && target != null)
        {
            _agent.SetDestination(target.position);

            yield return _pathUpdateDelay;
        }

        Stop();
    }
}