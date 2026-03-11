using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private const int PoolCapacity = 20;
    private const int PoolMaxSize = 100;

    [SerializeField] private Enemy _enemy;

    private PoolEnemies _pool;

    private void Awake()
    {
        _pool ??= gameObject.AddComponent<PoolEnemies>();
        _pool.Init(PoolCapacity, PoolMaxSize, _enemy);
    }

    public void SpawnEnemy(Vector3 spawnPosition)
    {
        Enemy enemy = _pool.GetInstance();
        enemy.Distroyd += DestroyEnemy;
        enemy.Init(spawnPosition);
    }

    private void DestroyEnemy(Enemy enemy)
    {
        enemy.Distroyd -= DestroyEnemy;
        _pool.ReturnInstance(enemy);
    }
}