using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Painter : MonoBehaviour
{
    [SerializeField] private LineRenderer _linePrefab;
    [SerializeField] private LayerMask _barrierLayer;
    [SerializeField] private float _minDistance = 0.1f;

    private LineRenderer _currentLine;
    private List<Vector3> _points = new();
    private Camera _cam;

    void Start() => _cam = Camera.main;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
            StartDrawing();

        if (Input.GetMouseButton(1))
            Draw();

        if (Input.GetMouseButtonUp(1)) 
            StopDrawing();
    }

    void StartDrawing()
    {
        _currentLine = Instantiate(_linePrefab, transform);
        _currentLine.gameObject.layer = _barrierLayer;
        _points.Clear();
    }

    void Draw()
    {
        Vector2 mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);

        if (_points.Count == 0 || Vector2.Distance(_points.Last(), mousePos) > _minDistance)
        {
            _points.Add(mousePos);
            _currentLine.positionCount = _points.Count;
            _currentLine.SetPositions(_points.ToArray());
        }
    }

    void StopDrawing()
    {
        if (_points.Count > 1)
        {
            AddNavMeshObstacle(_currentLine);
        }
        else
        {
            Destroy(_currentLine.gameObject);
        }
        _currentLine = null;
    }

    void AddNavMeshObstacle(LineRenderer line)
    {
        // Только физический барьер, без NavMesh
        BoxCollider2D collider = line.gameObject.AddComponent<BoxCollider2D>();
        collider.isTrigger = false; // Твердый барьер

        // Увеличиваем для покрытия всей линии
        collider.size = Vector2.one * 0.3f;

        // Враги будут физически сталкиваться
        Rigidbody2D rb = line.gameObject.AddComponent<Rigidbody2D>();
        rb.isKinematic = true; // Статический
        rb.bodyType = RigidbodyType2D.Static;
    }
}