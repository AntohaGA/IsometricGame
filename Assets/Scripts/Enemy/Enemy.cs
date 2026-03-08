using System;
using System.Collections;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamagable
{
    protected ZombieAnimator ZombieAnimator;
    protected ZombieMover ZombieMover;
    protected int StartHealth = 200;
    protected int Health;
    protected int Damage;

    public event Action<Enemy> Killed;

    private void Awake()
    {
        ZombieAnimator = GetComponent<ZombieAnimator>();
        ZombieMover = GetComponent<ZombieMover>();
    }

    private void OnEnable()
    {
        ZombieMover.enabled = true;
        GetComponent<Collider2D>().enabled = true;
        Killed += ZombieAnimator.Die;
        Health = StartHealth;
    }

    private void OnDisable()
    {
        ZombieMover.enabled = false;
        Killed -= ZombieAnimator.Die;
    }

    public void Init(Vector2 spawnspot)
    {
        transform.position = spawnspot;
        transform.rotation = Quaternion.identity;
        Health = StartHealth;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        ZombieAnimator.Hit();

        if (Health <= 0)
        {
            StartCoroutine(DeathSequence());
        }
    }

    private IEnumerator DeathSequence()
    {
        GetComponent<Collider2D>().enabled = false;
        ZombieMover.enabled = false;

        yield return new WaitForSeconds(2);

        Killed?.Invoke(this);
    }
}