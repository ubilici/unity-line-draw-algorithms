using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public int x;
    public int y;

    void OnMouseDown()
    {
        // select node
        FindObjectOfType<LineManager>().SelectNode(this);
        SetColor(Color.green);
    }

    public void SetColor(Color color)
    {
        GetComponent<SpriteRenderer>().color = color;
    }
}
