using UnityEngine;

public class PlayerKiller : MonoBehaviour
{
    private PlayerGirl _playerGirl;

    private void Start()
    {
        _playerGirl = GetComponent<PlayerGirl>();
        _playerGirl.Deceased += Die;
    }

    public void Die(PlayerGirl playerGirl)
    {
        _playerGirl.Deceased -= Die;
        Destroy(playerGirl.gameObject);
        Debug.Log("Игрок умер");
    }
}