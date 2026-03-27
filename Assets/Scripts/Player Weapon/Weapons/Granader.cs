using UnityEngine;
// Убираем наследование от MonoBehaviour для интерфейса. Просто MonoBehaviour.
public class Granader : MonoBehaviour
{
    [SerializeField] private PoolGrenades _poolGrenades;
    [SerializeField] private PoolExplosions _poolExplosions;
    [SerializeField] private Transform BulletSpawnerSpot;
    [SerializeField] private WeaponStats WeaponStats;

    public event System.Action OnShoot;

    // Метод называется так же - Shoot!
    public void Shoot()
    {
        Grenade grenade = _poolGrenades.GetInstance();
        grenade.Destroyed += (proj) => SpawnExplosion(proj.transform.position);
        grenade.Init(WeaponStats, BulletSpawnerSpot.position, BulletSpawnerSpot.right);

        OnShoot?.Invoke(); // Анимация, звук
    }

    private void SpawnExplosion(Vector3 position)
    {
        GrenadeExplosion explosion = _poolExplosions.GetInstance();
        explosion.transform.position = position;
        explosion.gameObject.SetActive(true);
        explosion.GetComponent<Lifetime>().Start(2f);
    }
}