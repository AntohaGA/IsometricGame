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
        Debug.Log("OnEnable() -- Enemy");
        Health.OnHit += ZombieAnimator.Hit;
        Health.Destroyd += ZombieAnimator.Die;
        Health.Destroyd += ZombieMover.Stop;
        Health.Destroyd += PlayerDetector.Stop;
        Health.Destroyd += HandleDeath;
        PlayerDetector.OnFoundPlayer += ZombieMover.GoToPlayer;
    }

    private void OnDisable()
    {
     /*   Health.OnHit -= ZombieAnimator.Hit;
        Health.Destroyd -= ZombieAnimator.Die;
        Health.Destroyd -= HandleDeath;
        Health.Destroyd -= ZombieMover.Stop;
        Health.Destroyd -= PlayerDetector.Stop;
        PlayerDetector.OnFoundPlayer -= ZombieMover.GoToPlayer;*/
    }

    public void Init(Vector2 spawnspot)
    {
        transform.position = spawnspot;
        transform.rotation = Quaternion.identity;
    }

    private void HandleDeath()
    {
        Health.OnHit -= ZombieAnimator.Hit;
        Health.Destroyd -= ZombieAnimator.Die;
        Health.Destroyd -= HandleDeath;
        Health.Destroyd -= ZombieMover.Stop;
        Health.Destroyd -= PlayerDetector.Stop;
        PlayerDetector.OnFoundPlayer -= ZombieMover.GoToPlayer;

        StartCoroutine(DeathSequence());
    }

    private IEnumerator DeathSequence()
    {
        yield return new WaitForSeconds(2);

        Destroyd?.Invoke(this);
    }
}