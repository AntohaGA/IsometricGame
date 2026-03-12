using UnityEngine;

public class PlayerKiller : MonoBehaviour
{
    public void Die()
    {
        Destroy(gameObject);
    }
}