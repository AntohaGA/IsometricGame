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

    private event Action OnKilled;
    public event Action<Enemy> Distroyd;

    private void Awake()
    {
        ZombieAnimator = GetComponent<ZombieAnimator>();
        ZombieMover = GetComponent<ZombieMover>();
    }

    private void OnEnable()
    {
        ZombieMover.enabled = true;
        GetComponent<Collider2D>().enabled = true;
        Health = StartHealth;
        OnKilled += ZombieAnimator.Die;
    }

    private void OnDisable()
    {
        ZombieMover.enabled = false;
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
            OnKilled?.Invoke();
            StartCoroutine(DeathSequence());
        }
    }

    private IEnumerator DeathSequence()
    {
        GetComponent<Collider2D>().enabled = false;
        ZombieMover.enabled = false;

        yield return new WaitForSeconds(2);

        Distroyd?.Invoke(this);
    }
}