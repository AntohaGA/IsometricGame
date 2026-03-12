using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCreator : MonoBehaviour
{
    private PlayerGirl _playerGirl;

    private void Start()
    {
        _playerGirl = FindFirstObjectByType<PlayerGirl>();
    }
}