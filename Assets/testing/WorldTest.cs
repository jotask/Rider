using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(EdgeCollider2D), typeof(LineRenderer))]
public class WorldTest : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        EdgeCollider2D edge = GetComponent<EdgeCollider2D>();
        LineRenderer line = GetComponent<LineRenderer>();

        Vector2[] edges = edge.points;
        Vector3[] points = new Vector3[edges.Length];
        line.positionCount = edges.Length;
        for (int i = 0; i < edges.Length; i++)
        {
            Vector2 a = edges[i];
            Vector3 b = new Vector3(a.x, a.y, 0f);
            points[i] = b;
            line.SetPosition(i, b);
        }
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

}