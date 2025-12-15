using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Run()
    {
        _animator.SetBool("isRun", true);
    }

    public void Die()
    {
        _animator.SetTrigger("Die");
    }

    public void Stand()
    {
        _animator.SetBool("isRun", false);
    }
}
