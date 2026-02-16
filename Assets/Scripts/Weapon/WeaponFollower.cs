using UnityEngine;

public class WeaponFollower : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Vector3 _rightHandOffset;
    [SerializeField] private Vector3 _leftHandOffset;

    private void LateUpdate()
    {
        if (_player == null) return;

        // Определяем, куда смотрит игрок
      //  bool isFacingLeft = IsPlayerFacingLeft();

     //   Vector3 handOffset = isFacingLeft ? _leftHandOffset : _rightHandOffset;

        // Учитываем только поворот, но не scale
     //   Vector3 worldOffset = _player.rotation * handOffset;
      //  transform.position = _player.position + worldOffset;
    }

    private bool IsPlayerFacingLeft()
    {
        return _player.localScale.x < 0;

        // ИЛИ, если используется Animator:
        // return _animator.GetBool("IsFacingLeft");
    }
}