using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class DamageZone : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _delay;

    private Coroutine _makeDamage;
    private Coroutine _makeGrow;
    private float _durationGrow = 2;
    private Vector3 _targetScale = new Vector3(5,5,5);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamagable player))
        {
            Debug.Log("Игрок зашёл");

            if (_makeDamage == null)
            {
                _makeDamage = StartCoroutine(MakeDamage(player));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IDamagable player))
        {
            Debug.Log("Игрок вышел");

            if (_makeDamage != null)
            {
                StopCoroutine(_makeDamage);
                _makeDamage = null;
            }
        }
    }

    private IEnumerator MakeDamage(IDamagable player)
    {
        WaitForSeconds delay = new WaitForSeconds(_delay);

        while (enabled)
        {
            Debug.Log("Делаю урон игроку");
            player.TakeDamage(_damage);

            yield return delay;
        }
    }

    public void StartGrow()
    {
        if(_makeGrow == null)
        {
            _makeGrow = StartCoroutine(ScaleOverTime());
        }
    }

    public IEnumerator ScaleOverTime()
    {
        Vector3 initialScale = transform.localScale;
        float elapsed = 0f;

        while (elapsed < _durationGrow)
        {
            transform.localScale = Vector3.Lerp(initialScale, _targetScale, elapsed / _durationGrow);
            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localScale = _targetScale;

        StopCoroutine(_makeGrow);
        _makeGrow = null;
    }
}