using Unity.Mathematics;
using UnityEngine;

public class Onda : MonoBehaviour
{
    LineRenderer lineRenderer;

    int numPoints = 500;
    float length = 10;

    float amplitud = 2.5f;
    float longitudOnda = 1.5f;
    float frecuencia = 0.5f;
    
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        if (numPoints < 2) numPoints = 2;
        lineRenderer.positionCount = numPoints;
    }

    void Update()
    {
        float startX = - length / 2;
        float step = length / (numPoints - 1);
        for (int i = 0; i < numPoints; i++)
        {
            float x = startX + i * step;
            float y = amplitud * Mathf.Sin((x / longitudOnda) + (Time.time * frecuencia));
            lineRenderer.SetPosition(i, new Vector3(x, y, 0));
        }
    }
}
