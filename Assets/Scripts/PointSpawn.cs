using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public class PointSpawn : MonoBehaviour
{
    [SerializeField] private int _count;
    [SerializeField] private float _delay;
    [SerializeField] private EnemySpawner _enemySpawner;

    private CapsuleCollider2D _capsuleCollider2D;
    private bool isSpawned = false;
    private Coroutine _coroutine;

    private void Awake()
    {
        _capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        _capsuleCollider2D.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isSpawned == false && _coroutine == null && collision.tag == "Player")
        {
            _coroutine = StartCoroutine(SpawnEnemyCoroutine());
            isSpawned = true;
        }
    }

    private IEnumerator SpawnEnemyCoroutine()
    {
        WaitForSeconds delay = new WaitForSeconds(_delay);
        int currentSpawn = _count;

        while (currentSpawn > 0)
        {
            _enemySpawner.SpawnEnemy(transform.position);
            currentSpawn--;

            yield return delay;
        }
    }
}
