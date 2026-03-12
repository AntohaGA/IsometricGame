using UnityEngine;

[ExecuteInEditMode]  // ✅ Работает в редакторе!
public class SnapToGrid : MonoBehaviour
{
    [SerializeField] private float _gridSize = 1f;

    void OnValidate()
    {
        Snap();
    }

#if UNITY_EDITOR
    void Update()
    {
        if (!Application.isPlaying)
            Snap();
    }
#endif

    void Snap()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Round(pos.x / _gridSize) * _gridSize;
        pos.y = Mathf.Round(pos.y / _gridSize) * _gridSize;
        pos.z = Mathf.Round(pos.z / _gridSize) * _gridSize;
        transform.position = pos;
    }
}