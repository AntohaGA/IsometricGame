using UnityEngine;

public class SimpleAccuracy : MonoBehaviour
{
    [Header("Accuracy")]
    [SerializeField] private float _standingSpread = 0f;   
    [SerializeField] private float _movingSpread;  

    private Rigidbody2D _rigidbody2D;
    private bool _isMoving;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _isMoving = _rigidbody2D.linearVelocity.magnitude > 0.1f;
    }

    public Vector2 GetDirection(Vector2 aimDirection)
    {
        float spread = _isMoving ? _movingSpread : _standingSpread;
        float randomAngle = Random.Range(-spread, spread);

        return Rotate(aimDirection, randomAngle);
    }

    private Vector2 Rotate(Vector2 dir, float degrees)
    {
        float rad = degrees * Mathf.Deg2Rad;

        return new Vector2(
            dir.x * Mathf.Cos(rad) - dir.y * Mathf.Sin(rad),
            dir.x * Mathf.Sin(rad) + dir.y * Mathf.Cos(rad)
        );
    }
}