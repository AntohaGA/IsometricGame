using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private BulletDetector _bulletDetector;

    public void TakeDamage(float damage)
    {
        _bulletDetector.TakeDamage(damage);
    }
}