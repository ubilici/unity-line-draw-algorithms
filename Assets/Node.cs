using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Vector2 nodePosition;

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
