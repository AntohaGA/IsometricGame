using System;
using System.Collections;
using UnityEngine;

public class Lifetime
{
    private Coroutine _coroutine;
    private MonoBehaviour _mono;
    private Action _onDestroy;

    public Lifetime(MonoBehaviour mono, Action onDestroy)
    {
        _mono = mono;
        _onDestroy = onDestroy;
    }

    public void Start(float lifeTime)
    {
        Stop();
        _coroutine = _mono.StartCoroutine(LifeTimer(lifeTime));
    }

    public void Stop()
    {
        if (_coroutine != null)
            _mono.StopCoroutine(_coroutine);
    }

    private IEnumerator LifeTimer(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);

        _onDestroy?.Invoke();
    }
}