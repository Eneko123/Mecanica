using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class SistemaParticulas : MonoBehaviour
{
    public GameObject prefabParticula;
    private List<Particulas> particulas = new List<Particulas>();
    private List<GameObject> graficos = new List<GameObject>();

    // Optimizacion 
    private Queue<GameObject> poolObjetos = new Queue<GameObject>();

    protected void Update()
    {
        // Loop de Simulacion
        for (int i = 0; i < particulas.Count; i++)
        {
            if (particulas[i].activa)
            {
                // Aplicar fuerzas e Integrar posiciones
                particulas[i].Resolve(Time.deltaTime);

                // Actualizar representacion grafica en Unity
                graficos[i].transform.position = particulas[i].posicion;
            }
            else
            {
                // Eliminar las muertas (Desactivar en pool)
                DesactivarParticula(i);
                i--; // Ajustar indice
            }
        }
    }

    public void SolicitarParticula(Vector3 pos, Vector3 vel, float vida)
    {
        // Logica simple de pool o creacion
        GameObject grafico;
        if (poolObjetos.Count > 0)
            grafico = poolObjetos.Dequeue();
        else
            grafico = Instantiate(prefabParticula);

        grafico.SetActive(true);

        // Buscar o crear objeto Particula correspondiente
        Particulas p = new Particulas();
        p.Inicializar(pos, vel, vida);
        particulas.Add(p);
        graficos.Add(grafico);
    }

    void DesactivarParticula(int indice)
    {
        poolObjetos.Enqueue(graficos[indice]);
        graficos[indice].SetActive(false);
        particulas.RemoveAt(indice);
        graficos.RemoveAt(indice);
    }
}