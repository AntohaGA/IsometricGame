using UnityEngine;

public class BulletMovement
{
    private Rigidbody2D _rigidbody2D;
    private Vector2 _direction;
    private int _speed;

    public BulletMovement(Rigidbody2D rigidbody2D)
    {
        _rigidbody2D = rigidbody2D;
    }

    public void Move(Vector2 direction, int speed)
    {
        _direction = direction.normalized;
        _speed = speed;
        _rigidbody2D.linearVelocity = _direction * _speed;
    }
}