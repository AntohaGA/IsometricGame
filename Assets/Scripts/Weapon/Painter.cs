using System.Collections.Generic;
using UnityEngine;

public class Painter : MonoBehaviour
{
    [SerializeField] private GameObject _wallPrefab;
    [SerializeField] private Grid _grid;
    [SerializeField] private float _cellSize = 1f;

    private Camera _cam;
    private bool _isDrawing = false;
    private HashSet<Vector3Int> _placedCells = new();

    void Start()
    {
        _cam = Camera.main;
        if (_grid == null) _grid = FindAnyObjectByType<Grid>();
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

        if (!_placedCells.Contains(currentCell))
        {
            PlaceWall(currentCell);
            _placedCells.Add(currentCell);
        }
    }

    private void PlaceWall(Vector3Int cell)
    {
        Vector3 cellCenter = _grid.CellToWorld(cell) + new Vector3(_cellSize * 0.5f, _cellSize * 0.5f, 0);
        cellCenter.z = 0f;
        GameObject wall = Instantiate(_wallPrefab, cellCenter, Quaternion.identity);
        SpriteRenderer spriteRenderer = wall.GetComponent<SpriteRenderer>();
    }

    private Vector3Int GetGridCell(Vector3 screenPos)
    {
        Vector3 worldPos = _cam.ScreenToWorldPoint(screenPos);
        worldPos.z = 0f;

        return _grid.WorldToCell(worldPos);
    }
}