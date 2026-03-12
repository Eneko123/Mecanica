using UnityEngine;
using System.Collections.Generic;

public class Pulso : MonoBehaviour
{
    LineRenderer lineRenderer;

    int numPoints = 500;
    float length = 10;

    float tiempoInicial = 1;
    float posicionInicial = 5;
    float amplitud = 3; // Amplitud del pulso
    float anchura = 0.15f; // Ancho del pulso

    KeyCode emitKey = KeyCode.Space;
    Transform emitPonit;

    struct PulsoData
    {
        public float tiempoInicial;
        public float posicionInicial;
        public float amplitud; // Amplitud del pulso
        public float anchura; // Ancho del pulso
    }

    List<PulsoData> pulsos = new List<PulsoData>();

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        if (numPoints < 2) numPoints = 2;
        lineRenderer.positionCount = numPoints;
    }

    void Update()
    {
        float Xp = posicionInicial + Time.time - tiempoInicial;
        float step = length / (numPoints - 1);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < numPoints; i++)
            {
                float x = Xp + i * step;
                float y = amplitud * Mathf.Sin((x / amplitud) + (Time.time * anchura));
                lineRenderer.SetPosition(i, new Vector3(x, y, 0));
            }
        }
    }

    void ExitImpulse()
    {
        PulsoData p = new PulsoData
        {
            tiempoInicial = Time.time,
            posicionInicial = posicionInicial,
            amplitud = amplitud,
            anchura = anchura
        };
    }
}
