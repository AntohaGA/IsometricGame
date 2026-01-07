using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    private float _damage;
    private float _lifeTime;
    private int _throughCount = 1;

    private float _speed;
    
    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Init(float lifeTime,float speed, int damage, int throughCount)
    {
        _lifeTime = lifeTime;
        _speed = speed;
        _damage = damage;
        _throughCount = throughCount;
        StartCoroutine(LifeTime());
    }

    private IEnumerator LifeTime()
    {
        float timer = 0f;

        while (timer < _lifeTime)
        {
            _rigidbody2D.linearVelocity = transform.TransformDirection(new Vector2(0, _speed));
            Debug.Log("speed - " + _speed);
            timer += Time.deltaTime;

            yield return null;
        }

        Destroy();
    }

    public float GetDamage()
    {
        return _damage;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}