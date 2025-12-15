using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(SpriteRenderer))]
public class FlipperAgent : MonoBehaviour
{
    private NavMeshAgent _agent;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        Flipp();
    }

    public void Flipp()
    {
        if (_agent.velocity.x > 0)
        {
            _spriteRenderer.flipX = false;
        }
        else if (_agent.velocity.x < 0)
        {
            _spriteRenderer.flipX = true;
        }
    }
}