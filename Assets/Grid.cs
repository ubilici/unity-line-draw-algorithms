using System.Collections;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public Vector2 gridSize;
    public float distanceBetweenNodes;
    public GameObject nodePrefab;

    private Node[,] nodes;

    void Start()
    {
        CreateGrid();
        MoveCamera();

        FindObjectOfType<LineManager>().SetNodeArray(nodes);
    }

    private void CreateGrid()
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
    }

    private void MoveCamera()
    {
        FindObjectOfType<Camera>().transform.position = new Vector3((gridSize.x * distanceBetweenNodes) / 2, (gridSize.y * distanceBetweenNodes) / 2, -10);
        FindObjectOfType<Camera>().orthographicSize = Mathf.Max(gridSize.x, gridSize.y) / 3;
    }
}
