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

    protected virtual void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnRate)
        {
            timer = 0f;
            EmitirParticula();
        }
    }

    protected virtual void EmitirParticula()
    {
        // Busca una partícula inactiva en el pool
        Particulas p = BuscarInactiva();

        if (p == null)
        {
            // Pool no lleno: crea una nueva
            if (pool.Count < maxParticulas)
            {
                GameObject go = Instantiate(particulaPrefab, PosicionEmision(), Quaternion.identity);
                p = go.GetComponent<Particulas>();
                pool.Add(p);
            }
            else
            {
                return; // Pool lleno y todas activas, no emitir
            }
        }

        p.gameObject.SetActive(true);
        p.Init(PosicionEmision(), VelocidadAleatoria(), aceleracion, vida);
    }

    // Devuelve una partícula inactiva del pool, o null si no hay
    private Particulas BuscarInactiva()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].gameObject.activeInHierarchy)
            {
                return pool[i];
            }
        }
        return null;
    }

    protected Vector3 PosicionEmision()
    {
        if (areaEmision <= 0f)
            return transform.position;

        // Área circular alrededor del emisor
        Vector2 offset = Random.insideUnitCircle * areaEmision;
        return transform.position + new Vector3(offset.x, offset.y, 0f);
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