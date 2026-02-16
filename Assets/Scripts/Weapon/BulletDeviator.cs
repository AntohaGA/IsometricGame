using UnityEngine;

public class BulletDeviator : MonoBehaviour
{
    [SerializeField] private float _startDeviationDegrees;

    public Vector2 CalculateDirection(Transform spot)
    {
        Vector2 baseDirection = spot.right.normalized;
        float deviation = Random.Range(-_startDeviationDegrees, _startDeviationDegrees);
        float deviationRad = deviation * Mathf.Deg2Rad;
        float angle = Mathf.Atan2(baseDirection.y, baseDirection.x) + deviationRad;

        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).normalized;
    }
}