using System;
using System.Collections;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamagable
{
    protected BulletDetector BulletDetector;
    protected ZombieAnimator ZombieAnimator;
    protected ZombieMover ZombieMover;
    protected int StartHealth = 200;
    protected int Health;

    public event Action OnKilled;
    public event Action<Enemy> Killed;

    private void Awake()
    {
        BulletDetector = GetComponent<BulletDetector>();
        ZombieAnimator = GetComponent<ZombieAnimator>();
        ZombieMover = GetComponent<ZombieMover>();
    }

    private void OnEnable()
    {
        BulletDetector.enabled = true;
        ZombieMover.enabled = true;
        BulletDetector.OnBulletDamage += TakeDamage;
        BulletDetector.OnBulletDamage += ZombieAnimator.Hit;
        OnKilled += ZombieAnimator.Die;
        Health = StartHealth;
    }

    private void OnDisable()
    {
        BulletDetector.enabled = false;
        ZombieMover.enabled = false;
        BulletDetector.OnBulletDamage -= TakeDamage;
        BulletDetector.OnBulletDamage -= ZombieAnimator.Hit;
        OnKilled -= ZombieAnimator.Die;
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

        if (Health <= 0)
        {
            StartCoroutine(DeathSequence());
        }
    }

    private IEnumerator DeathSequence()
    {
        OnKilled?.Invoke();
        BulletDetector.enabled = false;
        GetComponent<Collider2D>().enabled = false;
        ZombieMover.enabled = false;

        yield return new WaitForSeconds(2);

        Killed?.Invoke(this);
    }

    public virtual void Kill()
    {
        StopAllCoroutines();
        Killed?.Invoke(this);
    }
}