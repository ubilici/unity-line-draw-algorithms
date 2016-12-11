using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DrawAlgorithm
{
    DirectDraw,
    Bresenham
}

public class LineManager : MonoBehaviour
{
    public Color lineColor;
    public DrawAlgorithm drawAlgorithm;

    private Vector2 gridSize;
    private List<Node> selectedNodes;
    private Node[,] nodes;

    void Start()
    {
        selectedNodes = new List<Node>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DrawTest();
        }
    }

    public void SetNodeArray(Node[,] nodes)
    {
        this.nodes = nodes;
    }

    public void SetGridSize(Vector2 gridSize)
    {
        this.gridSize = gridSize;
    }

    public void SetDrawAlgorithm(int value)
    {
        switch (value)
        {
            case 0:
                drawAlgorithm = DrawAlgorithm.DirectDraw;
                break;
            case 1:
                drawAlgorithm = DrawAlgorithm.Bresenham;
                break;
        }
    }

    public void SelectNode(Node node)
    {
        selectedNodes.Add(node);

        if (selectedNodes.Count == 2)
        {
            var startPoint = selectedNodes[0];
            var endPoint = selectedNodes[1];

            var x1 = startPoint.x;
            var y1 = startPoint.y;
            var x2 = endPoint.x;
            var y2 = endPoint.y;

            switch (drawAlgorithm)
            {
                case DrawAlgorithm.DirectDraw:
                    DirectDraw(x1, x2, y1, y2);
                    break;

                case DrawAlgorithm.Bresenham:
                    Bresenham(x1, x2, y1, y2);
                    break;
            }
            
            selectedNodes[0].SetColor(Color.black);
            ClearSelectedNodes();
        }
    }

    public void ClearSelectedNodes()
    {
        selectedNodes.Clear();
    }

    public string DrawTest()
    {
        int maxSizeX = Mathf.RoundToInt(gridSize.x);
        int maxSizeY = Mathf.RoundToInt(gridSize.y);

        if(maxSizeX < 12 || maxSizeY < 12)
        {
            return "Grid size should be bigger than 12 * 12 to be tested";
        }

        int x1 = 0;
        int y1 = 0;
        int x2 = Random.Range(maxSizeX - 10, maxSizeX);
        int y2 = Random.Range(maxSizeY - 10, maxSizeY);

        string testResult;

        //testResult = "Draw lines between " + x1 + "," + y1 + " / " + x2 + "," + y2 + "\n";

        var watch1 = System.Diagnostics.Stopwatch.StartNew();
        for (int i = 0; i < 5000; i++)
        {
            DirectDraw(x1, x2, y1, y2);
        }
        watch1.Stop();
        var elapsedMs1 = watch1.ElapsedMilliseconds;

        testResult = "Elapsed time for DirectDraw: " + elapsedMs1 + "ms.\n\n";

        var watch2 = System.Diagnostics.Stopwatch.StartNew();
        for (int i = 0; i < 5000; i++)
        {
            Bresenham(x1, x2, y1, y2);
        }
        watch2.Stop();
        var elapsedMs2 = watch2.ElapsedMilliseconds;

        testResult += "Elapsed time for Bresenham: " + elapsedMs2 + "ms.\n";

        return testResult;
    }

    private void DirectDraw(int x1, int x2, int y1, int y2)
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
            PaintNode(x, y);

            x += xDifference;
            y += yDifference;

            numberOfNodes--;
        }
    }

    private void Bresenham(int x1, int x2, int y1, int y2)
    {
        int w = x2 - x1;
        int h = y2 - y1;

        int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
        if (w < 0) dx1 = -1; else if (w > 0) dx1 = 1;
        if (h < 0) dy1 = -1; else if (h > 0) dy1 = 1;
        if (w < 0) dx2 = -1; else if (w > 0) dx2 = 1;
        int longest = Mathf.Abs(w);
        int shortest = Mathf.Abs(h);
        if (!(longest > shortest))
        {
            longest = Mathf.Abs(h);
            shortest = Mathf.Abs(w);
            if (h < 0) dy2 = -1; else if (h > 0) dy2 = 1;
            dx2 = 0;
        }
        int numerator = longest >> 1;
        for (int i = 0; i <= longest; i++)
        {
            PaintNode(x1, y1);
            numerator += shortest;
            if (!(numerator < longest))
            {
                numerator -= longest;
                x1 += dx1;
                y1 += dy1;
            }
            else
            {
                x1 += dx2;
                y1 += dy2;
            }
        }
    }

    private void PaintNode(float x, float y)
    {
        nodes[Mathf.RoundToInt(x), Mathf.RoundToInt(y)].SetColor(lineColor);
    }

    private void PaintNode(int x, int y)
    {
        nodes[x, y].SetColor(lineColor);
    }
}
