using System.Collections.Generic;
using UnityEngine;

public class Emisor : MonoBehaviour
{
    [Header("Prefab")]
    public GameObject particulaPrefab;

    [Header("Emisión")]
    public float spawnRate = 0.5f;      // Segundos entre partículas
    public int maxParticulas = 30;

    [Header("Condiciones iniciales")]
    public Vector3 aceleracion = new Vector3(0f, -9.8f, 0f); // Gravedad por defecto
    public float vida = 3f;

    public Vector3 velInicial = new Vector3(0f, 5f, 0f);
    public float velVariacion = 2f;

    [Header("Área de emisión")]
    public float areaEmision = 2f;

    // Pool de partículas
    protected List<Particulas> pool = new List<Particulas>();
    private float timer;

    private void Start()
    {
        AniadirParticula(maxParticulas);
    }

    protected virtual void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnRate)
        {
            timer = 0f;
            EmitirParticula();
        }
    }

    protected virtual void AniadirParticula(int size)
    {
        for (int i = 0; i < size; i++)
        {
            GameObject p = Instantiate(particulaPrefab, PosicionEmision(), Quaternion.identity);
            p.SetActive(false);
            pool.Add(p.GetComponent<Particulas>());
        }
    }

    protected virtual void EmitirParticula()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].gameObject.activeInHierarchy)
            {
                pool[i].gameObject.SetActive(true);
                pool[i].Init(PosicionEmision(), VelocidadAleatoria(), aceleracion, vida);
                return; // Una partícula emitida, salimos
            }
        }

        // Si todas están activas y no hemos llegado al máximo, creamos una más
        if (pool.Count < maxParticulas)
        {
            AniadirParticula(1);
            pool[pool.Count - 1].gameObject.SetActive(true);
            pool[pool.Count - 1].Init(PosicionEmision(),VelocidadAleatoria(), aceleracion, vida);
        }
    }

    protected Vector3 PosicionEmision()
    {
        // Área circular alrededor del emisor
        Vector2 offset = Random.insideUnitCircle * areaEmision;
        return transform.position + new Vector3(offset.x, 0f, 0f);
    }

    protected Vector3 VelocidadAleatoria()
    {
        return velInicial + new Vector3(
            Random.Range(-velVariacion, velVariacion),
            Random.Range(-velVariacion, velVariacion),
            Random.Range(-velVariacion, velVariacion)
        );
    }
}