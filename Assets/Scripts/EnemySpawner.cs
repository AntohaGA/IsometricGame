using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private float _spawnDelay = 3;
    [SerializeField] private float _spawnOffset = 3;

    private Transform _transform;

    private void Start()
    {
        _transform = transform;
        StartCoroutine(SpawnEnemyCoroutine(_spawnDelay));
    }

    private IEnumerator SpawnEnemyCoroutine(float time)
    {
        WaitForSeconds delay = new WaitForSeconds(time);

        while (enabled)
        {
            float x = Random.Range(_transform.position.x - _spawnOffset, _transform.position.x + _spawnOffset);
            float y = Random.Range(_transform.position.y - _spawnOffset, _transform.position.y + _spawnOffset);
            Vector3 randomCoordinate = new Vector3(x, y, gameObject.transform.position.z);
            Instantiate(_enemy, randomCoordinate, Quaternion.identity);

            yield return delay;
        }
    }
}