using UnityEngine;

public class GrenadeSpawner : MonoBehaviour
{
    [SerializeField] private PoolGrenades _poolGrenades;
    [SerializeField] private PoolExplosions _poolExplosions;

    public void SpawnGrenade(WeaponStats stats, Vector3 pos, Vector2 dir)
    {
        Grenade grenade = _poolGrenades.GetInstance();
        grenade.Init(stats, pos, dir);

        // Подписываемся на уничтожение гранаты, чтобы спавнить взрыв
        grenade.Destroyed += (proj) => SpawnExplosion(proj.transform.position);
    }

    private void SpawnExplosion(Vector3 position)
    {
        GrenadeExplosion explosion = _poolExplosions.GetInstance();
        explosion.transform.position = position;
        explosion.gameObject.SetActive(true);

        // Запускаем время жизни взрыва
        explosion.GetComponent<Lifetime>().Start(2f);
    }
}