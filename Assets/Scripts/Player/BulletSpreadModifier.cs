using UnityEngine;

public class BulletSpreadModifier : MonoBehaviour
{
    //  [Header("Spread Settings")]
    //  [SerializeField] private float standingSpread = 0f;
    //  [SerializeField] private float movingSpread = 10f;

    private void ModifiedShoot(bool isMoving)
    {
        /*     // ✅ Добавляем разброс
             float spreadAngle = isMoving ? movingSpread : standingSpread;
             Vector2 direction = GetSpreadDirection(BulletSpawnerSpot.position, spreadAngle);

             // Вызываем оригинальный Shoot с модификацией
             weapon.BulletSpawner.GetBullet(weapon.WeaponStats, weapon.BulletSpawnerSpot, direction);
             weapon.OnShoot?.Invoke();*/
    }

    private Vector2 GetSpreadDirection(Vector3 targetPos, float spreadDegrees)
    {
        Vector2 perfectDir = (targetPos - transform.position).normalized;

        if (spreadDegrees <= 0) return perfectDir;

        float angle = Random.Range(-spreadDegrees, spreadDegrees) * Mathf.Deg2Rad;
        return new Vector2(
            perfectDir.x * Mathf.Cos(angle) - perfectDir.y * Mathf.Sin(angle),
            perfectDir.x * Mathf.Sin(angle) + perfectDir.y * Mathf.Cos(angle)
        );
    }
}