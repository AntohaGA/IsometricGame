using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Weapon : MonoBehaviour
{
    [SerializeField] protected Bullet _bulletPrefab;
    [SerializeField] protected Transform _bulletSpawner;
    [SerializeField] protected int _damage;

    protected SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public virtual void Shoot()
    {

    }

    public virtual void Equip(Vector3 position, Transform parent)
    {
        transform.SetParent(parent);
        transform.position = position;
        gameObject.SetActive(true);
    }

    public SpriteRenderer GetGunSprite() => _spriteRenderer;
}