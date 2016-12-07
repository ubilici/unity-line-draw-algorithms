using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour
{
    public Color lineColor;

    private List<Node> selectedNodes;
    private Node[,] nodes;

    void Start()
    {
        selectedNodes = new List<Node>();
    }

    public void SetNodeArray(Node[,] nodes)
    {
        this.nodes = nodes;
    }

    public void SelectNode(Node node)
    {
        selectedNodes.Add(node);

        if (selectedNodes.Count == 2)
        {
            var startPoint = selectedNodes[0].nodePosition;
            var endPoint = selectedNodes[1].nodePosition;

            var x1 = startPoint.x;
            var y1 = startPoint.y;
            var x2 = endPoint.x;
            var y2 = endPoint.y;

            DirectDraw(x1, x2, y1, y2);
            
            selectedNodes[0].SetColor(Color.black);
            selectedNodes.Clear();
        }
    }

    private void DirectDraw(float x1, float x2, float y1, float y2)
    {
        float dx = x2 - x1;
        float dy = y2 - y1;

        float numberOfNodes = Mathf.Abs(dx) > Mathf.Abs(dy) ? Mathf.Abs(dx) : Mathf.Abs(dy);

        float xDifference = dx / numberOfNodes;
        float yDifference = dy / numberOfNodes;

        float x = x1;
        float y = y1;

        while (numberOfNodes > 0)
        {
            nodes[Mathf.RoundToInt(x), Mathf.RoundToInt(y)].SetColor(lineColor);

            x += xDifference;
            y += yDifference;

            numberOfNodes--;
        }
    }
}
