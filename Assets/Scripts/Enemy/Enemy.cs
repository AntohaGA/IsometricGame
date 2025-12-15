using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private BulletDetector _bulletDetector;
    public void TakeDamage(float damage)
    {
        Debug.Log("вызов извне для взрыва");
        _bulletDetector.TakeDamage(damage);
    }
}