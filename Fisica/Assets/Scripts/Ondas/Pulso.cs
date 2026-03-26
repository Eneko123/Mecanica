using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(LineRenderer))]
public class Pulso : MonoBehaviour
{
    [SerializeField] int nPoints = 100;
    [SerializeField] float length = 10.0f;
    [SerializeField] float amplitude = 1.0f;
    [SerializeField] float waveLenght = 5.0f;
    [SerializeField] float waveSpeed = 3f;
    [SerializeField] float damping = 0.15f;
    [SerializeField] KeyCode emitKey = KeyCode.Space;
    [SerializeField] Transform emitPoint;
    LineRenderer lineRenderer;
    struct Pulse
    {
        public float originX;
        public float startTime;
        public float amplitude;
        public float width;
    }
    List<Pulse> pulseList = new List<Pulse>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        if (nPoints < 2) nPoints = 2;
        lineRenderer.positionCount = nPoints;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(emitKey))
        {
            EmitPulse();
        }
        DrawWave();
    }
    void EmitPulse()
    {
        Pulse p = new Pulse
        {
            originX = 0,
            startTime = Time.time,
            amplitude = amplitude,
            width = waveLenght
        };
        pulseList.Add(p);
    }
    void DrawWave()
    {
        float startX = -length * 0.5f;
        float step = length / (nPoints - 1);
        float currentTime = Time.time;
        for (int i = 0; i < nPoints; i++)
        {
            float x = startX + i * step;
            float y = 0.0f;
            for (int j = 0; j < pulseList.Count; j++)
            {
                Pulse p = pulseList[j];
                float age = currentTime - p.startTime;
                if (age < 0)
                {
                    continue;
                }
                float distRecorrida = age * waveSpeed;
                y += EvaluatePulse(x, p.originX + distRecorrida, amplitude, p.width,
                age);
                y += EvaluatePulse(x, p.originX - distRecorrida, amplitude,
                p.width, age);
            }
            lineRenderer.SetPosition(i, new Vector3(x, y, 0f));
        }
    }
    float EvaluatePulse(float x, float center, float amplitude, float width, float
    age)
    {
        float dist = x - center;
        float gaussian = Mathf.Exp(-(dist * dist) / (2f * width * width));
        return amplitude * gaussian;
    }
}
