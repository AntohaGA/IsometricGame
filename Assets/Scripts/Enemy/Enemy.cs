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

    public event Action OnKilled;
    public event Action<Enemy> Killed;

    private void Awake()
    {
        ZombieAnimator = GetComponent<ZombieAnimator>();
        ZombieMover = GetComponent<ZombieMover>();
    }

    private void OnEnable()
    {
        ZombieMover.enabled = true;
     //   DamageDetector.OnDamageDetect += TakeDamage;
     //   DamageDetector.OnDamageDetect += ZombieAnimator.Hit;
        OnKilled += ZombieAnimator.Die;
        Health = StartHealth;
    }

    private void OnDisable()
    {
     //   DamageDetector.enabled = false;
        ZombieMover.enabled = false;
      //  DamageDetector.OnDamageDetect -= TakeDamage;
      //  DamageDetector.OnDamageDetect -= ZombieAnimator.Hit;
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
     //   DamageDetector.enabled = false;
        GetComponent<Collider2D>().enabled = false;
        ZombieMover.enabled = false;

        yield return new WaitForSeconds(2);

        Killed?.Invoke(this);
    }
}