using System.Collections;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public Vector2 gridSize;
    public float distanceBetweenNodes;
    public GameObject nodePrefab;

    private bool gridGenerated;
    private Node[,] nodes;
    private LineManager lineManager;
    private Camera mainCamera;

    void Start()
    {
        lineManager = FindObjectOfType<LineManager>();
        mainCamera = FindObjectOfType<Camera>();
        gridGenerated = false;
    }

    public void CreateGrid(Vector2 gridSize)
    {
        DeleteCurrentGrid();
        this.gridSize = gridSize;
        NewGrid();
    }

    public void CreateGrid()
    {
        DeleteCurrentGrid();
        NewGrid();
    }

    private void NewGrid()
    {
        GenerateGrid();
        MoveCamera();
        lineManager.SetNodeArray(nodes);
        lineManager.SetGridSize(gridSize);
    }

    private void GenerateGrid()
    {
        Vector3 startPosition = Vector3.zero;
        Vector3 currentPosition = startPosition;

        nodes = new Node[Mathf.RoundToInt(gridSize.x), Mathf.RoundToInt(gridSize.y)];

        for (int y = 0; y < gridSize.y; y++)
        {
            for (int x = 0; x < gridSize.x; x++)
            {
                var node = Instantiate(nodePrefab, transform).GetComponent<Node>();
                node.transform.name = x + " " + y;
                node.transform.position = currentPosition;
                node.x = x;
                node.y = y;
                nodes[x, y] = node;
                currentPosition.x += distanceBetweenNodes;
            }
            currentPosition.y += distanceBetweenNodes;
            currentPosition.x = startPosition.x;
        }

        gridGenerated = true;
    }

    private void DeleteCurrentGrid()
    {
        if (gridGenerated)
        {
            foreach (var node in nodes)
            {
                Destroy(node.gameObject);
            }
            lineManager.ClearSelectedNodes();
        }
    }

    private void MoveCamera()
    {
        mainCamera.transform.position = new Vector3((gridSize.x * distanceBetweenNodes) / 2, (gridSize.y * distanceBetweenNodes) / 2, -10);
        mainCamera.orthographicSize = Mathf.Max(gridSize.x, gridSize.y) / 3;
    }
}
