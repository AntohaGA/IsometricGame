using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class SpotSpawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private int _countEnemy = 5;

    private BoxCollider2D _boxCollider2D;

    private Vector2 _min;
    private Vector2 _max;
    private bool _isSpawned = false;

    private void Start()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();

        Vector2 offset = _boxCollider2D.offset;
        Vector2 size = _boxCollider2D.size;
        Vector2 objectPosition = transform.position;

        _min = objectPosition + offset - size * 0.5f;
        _max = objectPosition + offset + size * 0.5f;

        StartLevelSpawn();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && _isSpawned == false)
        {
            StartCoroutine(SpawnEnemys(1, _countEnemy));
            _isSpawned = true;
            Debug.Log("Player come");
        }
    }

    private void StartLevelSpawn()
    {
        StartCoroutine(SpawnEnemys(0, 10));
    }

    private IEnumerator SpawnEnemys(float delaySpawn, int count)
    {
        WaitForSeconds delay = new WaitForSeconds(delaySpawn);

        while (count != 0)
        {
            float x = Random.Range(_min.x, _max.x);
            float y = Random.Range(_min.y, _max.y);

            Vector2 spawnPosition = new Vector2(x, y);
            Instantiate(_enemy, spawnPosition, Quaternion.identity);
            count--;

            yield return delay;
        }
    }
}