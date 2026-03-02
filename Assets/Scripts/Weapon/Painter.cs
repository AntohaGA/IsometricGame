using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
        if (Input.GetMouseButtonDown(1)) StartDrawing();
        if (Input.GetMouseButton(1)) Draw();
        if (Input.GetMouseButtonUp(1)) StopDrawing();
    }

    void StartDrawing()
    {
        _currentLine = Instantiate(_linePrefab, transform);
        _currentLine.gameObject.layer = Mathf.RoundToInt(_barrierLayer); // ✅ Правильный слой
        _points.Clear();
    }

    void Draw()
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

    void StopDrawing()
    {
        if (_points.Count > 1)
        {
            CreateBarrierLine(); // ✅ Новая функция
        }
        else if (_currentLine != null)
        {
            Destroy(_currentLine.gameObject);
        }
        _currentLine = null;
        _points.Clear();
    }

    void CreateBarrierLine()
    {
        // ✅ Создаем отдельный объект для барьера
        GameObject barrier = new GameObject("BarrierLine");
        barrier.layer = Mathf.RoundToInt(_barrierLayer);

        // 1. Копируем LineRenderer для визуала
        LineRenderer visualLine = barrier.AddComponent<LineRenderer>();
        visualLine.material = _currentLine.material;
        visualLine.startWidth = _currentLine.startWidth;
        visualLine.endWidth = _currentLine.endWidth;
        visualLine.positionCount = _points.Count;
        visualLine.SetPositions(_points.ToArray());

        // 2. ✅ EdgeCollider2D для точной линии!
        EdgeCollider2D edgeCollider = barrier.AddComponent<EdgeCollider2D>();
        edgeCollider.isTrigger = false;
        edgeCollider.points = Array.ConvertAll(_points.ToArray(), p => (Vector2)p);

        // 3. Rigidbody2D статический
      /*  Rigidbody2D rb = barrier.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;
        rb.simulated = false;*/

        // 4. Уничтожаем временную линию
      //  Destroy(_currentLine.gameObject);
    }
}