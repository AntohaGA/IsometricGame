using System;
using System.Collections;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDestroyble
{
    private Health _health;

    protected ZombieAnimator ZombieAnimator;
    protected ZombieMover ZombieMover;

    public event Action OnDestroy;
    public event Action<Enemy> Distroyd;

    private void Awake()
    {
        ZombieAnimator = GetComponent<ZombieAnimator>();
        ZombieMover = GetComponent<ZombieMover>();
        _health = GetComponent<Health>();
        _health.OnDead += HandleDeath;
    }

    private void OnEnable()
    {
        ZombieMover.enabled = true;
        GetComponent<Collider2D>().enabled = true;
        _health.OnHit += ZombieAnimator.Hit;
        _health.OnDead += ZombieAnimator.Die;
    }

    private void OnDisable()
    {
        ZombieMover.enabled = false;
        _health.OnHit -= ZombieAnimator.Hit;
        _health.OnDead -= ZombieAnimator.Die;
    }

    public void Init(Vector2 spawnspot)
    {
        transform.position = spawnspot;
        transform.rotation = Quaternion.identity;
    }

    private void HandleDeath()
    {
        OnDestroy?.Invoke();
        StartCoroutine(DeathSequence());
    }

    private IEnumerator DeathSequence()
    {
        GetComponent<Collider2D>().enabled = false;
        ZombieMover.enabled = false;

        yield return new WaitForSeconds(2);

        Distroyd?.Invoke(this);
    }
}