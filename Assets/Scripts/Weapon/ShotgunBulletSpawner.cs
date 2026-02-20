using UnityEngine;

public class ShotgunBulletSpawner : BulletSpawner
{
    [SerializeField] private int _pelletCount;
    [SerializeField] private float _coneHalfAngle;

    public override void SpawnBullet(WeaponStats bulletData, Transform from)
    {
        Vector3 position = from.position;

        for (int i = 0; i < _pelletCount; i++)
        {
            GameObject tempObj = new GameObject("TempPelletSpawner");
            Transform rotatedSpawner = tempObj.transform;
            float angleOffset = Random.Range(-_coneHalfAngle, _coneHalfAngle);

            rotatedSpawner.SetPositionAndRotation(position, from.rotation * Quaternion.Euler(0, 0, angleOffset));
            base.SpawnBullet(bulletData, rotatedSpawner);
            DestroyImmediate(tempObj);
        }
    }
}