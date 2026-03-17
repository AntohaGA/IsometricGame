using System;
using System.Collections;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    private Health _health;

    protected ZombieAnimator ZombieAnimator;
    protected ZombieMover ZombieMover;

    public event Action<Enemy> Destroyd;

    private void Awake()
    {
        ZombieAnimator = GetComponent<ZombieAnimator>();
        ZombieMover = GetComponent<ZombieMover>();
        _health = GetComponent<Health>();
        _health.Destroyd += HandleDeath;
    }

    private void OnEnable()
    {
        GetComponent<Collider2D>().enabled = true;
        ZombieMover.enabled = true;

        _health.OnHit += ZombieAnimator.Hit;
        _health.Destroyd += ZombieAnimator.Die;
        _health.Destroyd += HandleDeath;
    }

    private void OnDisable()
    {
        _health.OnHit -= ZombieAnimator.Hit;
        _health.Destroyd -= ZombieAnimator.Die;
        _health.Destroyd -= HandleDeath;
    }

    public void Init(Vector2 spawnspot)
    {
        transform.position = spawnspot;
        transform.rotation = Quaternion.identity;
    }

    private void HandleDeath()
    {
        StartCoroutine(DeathSequence());
    }

    private IEnumerator DeathSequence()
    {
        GetComponent<Collider2D>().enabled = false;
        ZombieMover.enabled = false;

        yield return new WaitForSeconds(2);

        Destroyd?.Invoke(this);
    }
}