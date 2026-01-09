using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    private float _damage;
    private float _lifeTime;
    private float _speed;
    
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }  

    public virtual void Fly(float lifeTime,float speed, int damage)
    {
        _lifeTime = lifeTime;
        _speed = speed;
        _damage = damage;
        StartCoroutine(LifeTime());
    }

    private IEnumerator LifeTime()
    {
        float timer = 0f;

        Debug.Log("speed - " + _speed + "lifeTime - " + _lifeTime);

        while (timer < _lifeTime)
        {
            _rigidbody2D.linearVelocity = transform.TransformDirection(new Vector2(0, _speed));
            Debug.Log("speed - " + _speed);
            timer += Time.deltaTime;

            yield return null;
        }

        Destroy();
    }

    public float GetDamage() => _damage;

    public void Destroy()
    {
        Destroy(gameObject);
    }
}