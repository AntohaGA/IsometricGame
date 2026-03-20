using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ZombieAnimator : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Run()
    {
        _animator.SetBool("isRun", true);
    }

    public void Stand()
    {
        _animator.SetBool("isRun", false);
    }

    public void Hit()
    {
        _animator.SetTrigger("isHit");
    }

    public void Die()
    {
        _animator.SetBool("isDead", true);
    }

    public void ResetState()
    {
        _animator.SetBool("isDead", false);
        _animator.SetBool("isRun", false);
    }
}