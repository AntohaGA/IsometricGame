using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerDetector : MonoBehaviour
{
    [SerializeField] private LayerMask _targetLayerMask;

    private Collider2D _detectorCollider;

    public event Action<Transform> OnFoundPlayer;

    private void Awake()
    {
        _detectorCollider = GetComponent<Collider2D>();
        _detectorCollider.isTrigger = true;
    }

    private void OnEnable()
    {
        _detectorCollider.enabled = true;
    }

    private void OnDisable()
    {
        _detectorCollider.enabled = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!_detectorCollider.enabled)
            return;

        if (IsInTargetLayer(collision.gameObject))
        {
            _detectorCollider.enabled = false;
            OnFoundPlayer?.Invoke(collision.transform);
        }
    }

    private bool IsInTargetLayer(GameObject obj)
    {
        return (_targetLayerMask.value & (1 << obj.layer)) > 0;
    }

    public void Stop()
    {
        _detectorCollider.enabled = false;
    }
}