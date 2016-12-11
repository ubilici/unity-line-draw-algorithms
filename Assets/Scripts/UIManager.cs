using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public InputField sizeX;
    public InputField sizeY;
    public Text testResults;

    private GridManager gridManager;
    private LineManager lineManager;

    private void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
        lineManager = FindObjectOfType<LineManager>();
    }

    public void GenerateGrid()
    {
        if (sizeX != null && sizeY != null)
        {
            int x, y;
            if (int.TryParse(sizeX.text, out x) && int.TryParse(sizeY.text, out y))
            {
                gridManager.CreateGrid(new Vector2(x, y));
            }
        }
    }

    public void ClearGrid()
    {
        gridManager.CreateGrid();
    }

    public void SetDrawAlgorithm(int value)
    {
        lineManager.SetDrawAlgorithm(value);
    }

    public void DrawTest()
    {
        testResults.text = lineManager.DrawTest();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
