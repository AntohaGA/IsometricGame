using System;
using System.Collections;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected Health Health;
    [SerializeField] protected ZombieAnimator ZombieAnimator;
    [SerializeField] protected ZombieMover ZombieMover;
    [SerializeField] protected PlayerDetector PlayerDetector;
    [SerializeField] protected Damager Damager;

    public event Action<Enemy> Destroyd;

    private void OnEnable()
    {
        Subscribe();
    }

    private void OnDisable()
    {
        Unsubscribe();
    }

    public void Init(Vector2 spawnspot)
    {
        transform.position = spawnspot;
        transform.rotation = Quaternion.identity;
        ZombieAnimator.Stand();
    }

    private void Subscribe()
    {
        Health.OnHit += ZombieAnimator.Hit;
        Health.Destroyd += ZombieAnimator.Die;
        Health.Destroyd += ZombieMover.Stop;
        Health.Destroyd += PlayerDetector.Stop;
        Health.Destroyd += HandleDeath;
        ZombieMover.OnRun += ZombieAnimator.Run;
        PlayerDetector.OnFoundPlayer += ZombieMover.GoTo;
    }

    private void Unsubscribe()
    {
        Health.OnHit -= ZombieAnimator.Hit;
        Health.Destroyd -= ZombieAnimator.Die;
        Health.Destroyd -= HandleDeath;
        Health.Destroyd -= ZombieMover.Stop;
        Health.Destroyd -= PlayerDetector.Stop;
        ZombieMover.OnRun -= ZombieAnimator.Run;
        PlayerDetector.OnFoundPlayer -= ZombieMover.GoTo;
    }

    private void HandleDeath()
    {
        Unsubscribe();
        StartCoroutine(DeathSequence());
    }

    private IEnumerator DeathSequence()
    {
        yield return new WaitForSeconds(2);

        Destroyd?.Invoke(this);
    }
}