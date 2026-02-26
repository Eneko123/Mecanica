using System.Collections.Generic;
using UnityEngine;

public class Emisor : MonoBehaviour
{
    public GameObject particulaPrefab;
    public float spawnRate = 0.1f;
    private float timer;
    public List<Particulas> particulas;
    public int maxParticulas = 10;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnRate)
        {
            // Pooling pocho
            // Si no hay suficientes particulas, crea una nueva
            if (particulas.Count <= maxParticulas)
            {
                GameObject p = Instantiate(particulaPrefab, transform.position, Quaternion.identity);
                p.SetActive(true);
                particulas.Add(p.GetComponent<Particulas>());
                timer = 0;
            }
            // Si ya hay suficientes particulas, reutiliza una que no esté activa
            else
            {
                // Busca una particula inactiva
                for (int i = 0; i < particulas.Count; i++)
                {
                    if (!particulas[i].gameObject.activeInHierarchy)
                    {
                        particulas[i].ResetParticle();
                        particulas[i].gameObject.SetActive(true);
                        timer = 0;
                        break;
                    }
                }
            }
        }
    }
}
