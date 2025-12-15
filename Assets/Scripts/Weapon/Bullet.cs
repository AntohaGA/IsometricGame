using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    private float _damage;

    private Rigidbody2D _rigidbody2D;
    private float _speed;
    private float _lifeTime;
    private int _throughCount = 1;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        StartCoroutine(LifeTime(2, 10));
    }

    public void Init(float timeLife, float speed, int damage, int throughCount)
    {
        _speed = speed;
        _lifeTime = timeLife;
        _damage = damage;
        _throughCount = throughCount;
    }

    private IEnumerator LifeTime(float _lifeTime, float _speed)
    {
        float timer = 0f;

        while (timer < _lifeTime)
        {
            _rigidbody2D.linearVelocity = transform.TransformDirection(new Vector2(0, _speed));
            timer += Time.deltaTime;

            yield return null;
        }

        Destroy(gameObject);
    }

    public float GetDamage()
    {
        return _damage;
    }
}