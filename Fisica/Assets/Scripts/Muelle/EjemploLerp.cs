using System.Collections;
using UnityEngine;

public class EjemploLerp : MonoBehaviour
{
    public Vector3 a, b;
    public Color c1, c2;

    public float duration;
    public Material nat;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(MoveLoop());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator MoveLoop()
    {
        yield return LerpCorrutine(a, b, c1, c2);
        yield return LerpCorrutine(b, a, c2, c1);
    }
    IEnumerator LerpCorrutine(Vector3 posInit, Vector3 posEnd, Color init, Color end) { 
        float time = 0;
        while (time < duration)
        {
            float t = time / duration; // Normalizar el tiempo para que vaya de 0 a 1
            transform.position = Vector3.Lerp(posInit, posEnd, t);
            nat.color = Color.Lerp(init, end, t);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = posEnd;
        nat.color = end;
    }
}
