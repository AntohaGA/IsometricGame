using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(NavMeshObstacle))]
public class Wall : MonoBehaviour, IDamagable
{
    [SerializeField] private int _maxHealth = 500;

    private int _health;

    private void Awake()
    {
        _health = _maxHealth;
    }
    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }
}