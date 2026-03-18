using System;
using System.Collections;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected Health Health;
    [SerializeField] protected ZombieAnimator ZombieAnimator;
    [SerializeField] protected ZombieMover ZombieMover;

    public event Action<Enemy> Destroyd;

    private void OnEnable()
    {
        ZombieMover.enabled = true;
        Health.OnHit += ZombieAnimator.Hit;
        Health.Destroyd += ZombieAnimator.Die;
        Health.Destroyd += HandleDeath;
    }

    private void OnDisable()
    {
        Health.OnHit -= ZombieAnimator.Hit;
        Health.Destroyd -= ZombieAnimator.Die;
        Health.Destroyd -= HandleDeath;
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
        ZombieMover.enabled = false;

        yield return new WaitForSeconds(2);

        Destroyd?.Invoke(this);
    }
}