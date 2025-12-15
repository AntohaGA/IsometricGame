using UnityEngine;
using UnityEngine.SceneManagement;

public class EnderLevel1 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene("Level1");
    }
}