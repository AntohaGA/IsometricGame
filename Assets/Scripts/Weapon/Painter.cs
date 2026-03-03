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
    [SerializeField] private Material _brickMaterial; // ✅ Кирпичный материал

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
        _currentLine.gameObject.layer = Mathf.RoundToInt(_barrierLayer);

        // ✅ Кирпичная текстура!
        _currentLine.material = _brickMaterial;
        _currentLine.startWidth = 0.4f;  // ✅ Толще для стен!
        _currentLine.endWidth = 0.4f;

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
        visualBarrier.layer = Mathf.RoundToInt(_barrierLayer);

        LineRenderer line = visualBarrier.AddComponent<LineRenderer>();
        line.material = _currentLine.material;
        line.startWidth = 0.2f;
        line.SetPositions(_points.ToArray());

        EdgeCollider2D edge = visualBarrier.AddComponent<EdgeCollider2D>();
        edge.points = Array.ConvertAll(_points.ToArray(), p => (Vector2)p);

        // 2. ДЛИННЫЕ NavMeshObstacle (ТОЛЬКО 3-5 штук!)
        CreateLongObstacles();

    }

    private void CreateLongObstacles()
    {
        const float MAX_SEGMENT_LENGTH = 2.0f; // ✅ Максимум 2 юнита на сегмент
        const int MAX_OBSTACLES = 5; // ✅ Максимум 5 препятствий

        int obstaclesCreated = 0;

        for (int i = 0; i < _points.Count - 1 && obstaclesCreated < MAX_OBSTACLES;)
        {
            Vector3 startPoint = _points[i];
            int endIndex = i + 1;

            // ✅ Собираем длинный сегмент (до 2 юнитов)
            float totalLength = 0;
            while (endIndex < _points.Count && totalLength < MAX_SEGMENT_LENGTH)
            {
                totalLength += Vector3.Distance(_points[endIndex - 1], _points[endIndex]);
                endIndex++;
            }

            // Создаем ДЛИННОЕ препятствие
            CreateLongObstacle(i, endIndex - 1);
            obstaclesCreated++;
            i = endIndex - 1; // Пропускаем обработанные точки
        }
    }

    private void CreateLongObstacle(int startIndex, int endIndex)
    {
        // 1. Центр и направление линии
        Vector3 center = Vector3.zero;
        Vector3 direction = Vector3.zero;

        for (int i = startIndex; i <= endIndex; i++)
            center += _points[i];
        center /= (endIndex - startIndex + 1);

        if (endIndex > startIndex)
            direction = (_points[endIndex] - _points[startIndex]).normalized;

        // 2. Создаем объект
        GameObject obstacleGO = new GameObject("LongObstacle");
        obstacleGO.transform.position = center;

        // 3. ✅ ПОВОРАЧИВАЕМ по направлению линии!
        if (direction != Vector3.zero)
        {
            // Вычисляем угол линии
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            obstacleGO.transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        // 4. NavMeshObstacle
        NavMeshObstacle obstacle = obstacleGO.AddComponent<NavMeshObstacle>();
        obstacle.carving = true;
        obstacle.carveOnlyStationary = false;
        obstacle.shape = NavMeshObstacleShape.Box;

        // 5. ✅ ДЛИННАЯ коробка ПО ВСЕЙ ЛИНИИ
        float lineLength = 0;
        for (int i = startIndex; i < endIndex; i++)
            lineLength += Vector3.Distance(_points[i], _points[i + 1]);

        obstacle.size = new Vector3(lineLength * 0.8f, 0.4f, 0.5f); // Длина x Ширина x Глубина

        Rigidbody2D rb = obstacleGO.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;
    }
}