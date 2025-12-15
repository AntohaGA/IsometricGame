using UnityEngine;

public class PlayerToucher : MonoBehaviour
{
    [SerializeField] private float _damage;

    private float damageTimer = 0f;

    private void Awake()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            damageTimer += Time.deltaTime;

            if (damageTimer >= 0.1f) 
            {
                other.gameObject.GetComponent<PlayerGirl>().TakeDamage(_damage);

                damageTimer = 0f;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            damageTimer = 0f;
        }
    }
}