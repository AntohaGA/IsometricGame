using UnityEngine;

public class FollowerCamera : MonoBehaviour
{
    private Transform _target;

    private void Awake()
    {
        _target = FindFirstObjectByType<PlayerGirl>().transform;
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(_target.position.x, _target.position.y, transform.position.z);
    }
}