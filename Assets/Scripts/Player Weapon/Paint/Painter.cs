using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Painter : MonoBehaviour
{
    [SerializeField] private GameObject _wallPrefab;
    [SerializeField] private Grid _grid;
    [SerializeField] private float _cellSize = 1f;

    private Camera _cam;
    private bool _isDrawing = false;

    // Вместо HashSet используем Dictionary:
    // Ключ - координата клетки (Vector3Int)
    // Значение - ссылка на созданный объект стены (GameObject)
    private Dictionary<Vector3Int, GameObject> _placedWalls = new Dictionary<Vector3Int, GameObject>();

    void Start()
    {
        _cam = Camera.main;

        if (_grid == null)
            _grid = FindAnyObjectByType<Grid>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) _isDrawing = true;
        if (Input.GetMouseButton(1)) DrawDirectly();
        if (Input.GetMouseButtonUp(1)) _isDrawing = false;
    }

    private void DrawDirectly()
    {
        if (!_isDrawing || _wallPrefab == null)
            return;

        Vector3Int currentCell = GetGridCell(Input.mousePosition);

        // Проверяем: если в словаре нет ключа с такой координатой, значит место свободно.
        if (!_placedWalls.ContainsKey(currentCell))
        {
            PlaceWall(currentCell);
        }
    }

    private void PlaceWall(Vector3Int cell)
    {
        Vector3 cellCenter = _grid.CellToWorld(cell) + new Vector3(_cellSize * 0.5f, _cellSize * 0.5f, 0);
        cellCenter.z = 0f;

        // Создаем стену
        GameObject wall = Instantiate(_wallPrefab, cellCenter, Quaternion.identity);

        // Добавляем в словарь: Координата -> Объект
        _placedWalls.Add(cell, wall);

        // --- НОВАЯ ЛОГИКА ---
        // Находим компонент Wall на созданном объекте и подписываемся на его событие OnDestroyed.
        Wall wallComponent = wall.GetComponent<Wall>();
        if (wallComponent != null)
        {
            wallComponent.DestroyThis += HandleWallDestroyed;
        }
    }

    // --- НОВЫЙ МЕТОД ---
    // Этот метод будет вызван, когда любая стена отправит сигнал о своем уничтожении.
    private void HandleWallDestroyed(Wall destroyedWall)
    {
        // Ищем координату по объекту стены и удаляем её из словаря.
        foreach (var pair in _placedWalls)
        {
            if (pair.Value == destroyedWall.gameObject)
            {
                // Удаляем запись из словаря, чтобы место стало доступным для рисования.
                _placedWalls.Remove(pair.Key);

                // Отписываемся от события, чтобы избежать утечек памяти.
                destroyedWall.DestroyThis -= HandleWallDestroyed;
                break;
            }
        }
    }

    private Vector3Int GetGridCell(Vector3 screenPos)
    {
        Vector3 worldPos = _cam.ScreenToWorldPoint(screenPos);
        worldPos.z = 0f;

        return _grid.WorldToCell(worldPos);
    }
}