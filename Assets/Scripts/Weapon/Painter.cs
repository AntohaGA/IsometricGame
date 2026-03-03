using System;
using System.Collections.Generic;
using System.Linq;
using Unity.AI.Navigation;
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

    private void Update()
    {
        if (Input.GetMouseButtonDown(1)) StartDrawing();
        if (Input.GetMouseButton(1)) Draw();
        if (Input.GetMouseButtonUp(1)) StopDrawing();
    }

    private void StartDrawing()
    {
        _currentLine = Instantiate(_linePrefab, transform);
        _currentLine.gameObject.layer = Mathf.RoundToInt(_barrierLayer); // ✅ Правильный слой
        _points.Clear();
    }

    private void Draw()
    {
        Vector3 mousePos = _cam.ScreenToWorldPoint(Input.mousePosition); // ✅ Vector3
        mousePos.z = 0;

        if (_points.Count == 0 || Vector3.Distance(_points.Last(), mousePos) > _minDistance)
        {
            _points.Add(mousePos);
            _currentLine.positionCount = _points.Count;
            _currentLine.SetPositions(_points.ToArray()); // ✅ Работает с Vector3
        }
    }

    private void StopDrawing()
    {
        if (_points.Count > 1)
        {
            CreateCarveBarrier(); // ✅ Новая функция
        }
        else if (_currentLine != null)
        {
            Destroy(_currentLine.gameObject);
        }
        _currentLine = null;
        _points.Clear();
    }

    private void CreateCarveBarrier()
    {
        // 1. ВИЗУАЛ (одна линия)
        GameObject visualBarrier = new GameObject("VisualBarrier");
        LineRenderer line = visualBarrier.AddComponent<LineRenderer>();
        line.material = _currentLine.material;
        line.startWidth = 0.2f;
        line.SetPositions(_points.ToArray());

        EdgeCollider2D edge = visualBarrier.AddComponent<EdgeCollider2D>();
        edge.points = Array.ConvertAll(_points.ToArray(), p => (Vector2)p);

        // 2. NavMesh препятствия (много маленьких)
        for (int i = 0; i < _points.Count; i += 2) // Каждый 2-й точка = оптимизация
        {
            GameObject obstacleGO = new GameObject("NavObstacle");
            obstacleGO.transform.position = _points[i];

            NavMeshObstacle obstacle = obstacleGO.AddComponent<NavMeshObstacle>();
            obstacle.carving = true;
            obstacle.carveOnlyStationary = false;
            obstacle.size = new Vector3(0.2f, 0.2f, 0.2f);
        }
    }
}