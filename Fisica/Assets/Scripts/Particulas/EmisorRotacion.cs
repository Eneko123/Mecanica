using System.Drawing;
using UnityEngine;

public class EmisorRotacion : Emisor
{
    [Header("Rotación inicial")]
    public Vector3 velAngularInicial = new Vector3(90f, 90f, 90f); 
    public float velAngularVariacion = 45f;

    public Vector3 acelAngularInicial = new Vector3(0f, 0f, 0f);    

    [Header("Física rotación")]
    public float masa = 1f;

    void AniadirParticulaRotatoria(int size)
    {
        for (int i = 0; i < size; i++)
        {
            GameObject p = Instantiate(particulaPrefab, PosicionEmision(), Quaternion.identity);
            p.SetActive(false);
            pool.Add(p.GetComponent<ParticulasRotatorias>());
        }
    }

    protected override void EmitirParticula()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].gameObject.activeInHierarchy)
            {
                pool[i].gameObject.SetActive(true);
                // Cambiar el tipo a ParticulasRotatorias antes de llamar a InitRotacion
                if (pool[i] is ParticulasRotatorias pr)
                {
                    pr.InitRotacion(
                        PosicionEmision(),
                        VelocidadAleatoria(),
                        aceleracion,
                        vida,
                        masa,
                        VelAngularAleatoria(),
                        acelAngularInicial
                    );
                }
                return; // Una partícula emitida, salimos
            }
        }

        // Si todas están activas y no hemos llegado al máximo, creamos una más
        if (pool.Count < maxParticulas)
        {
            AniadirParticula(1);
            pool[pool.Count - 1].gameObject.SetActive(true);
            pool[pool.Count - 1].gameObject.SetActive(true);
            // Cambiar el tipo a ParticulasRotatorias antes de llamar a InitRotacion
            if (pool[pool.Count - 1] is ParticulasRotatorias pr)
            {
                pr.InitRotacion(
                    PosicionEmision(),
                    VelocidadAleatoria(),
                    aceleracion,
                    vida,
                    masa,
                    VelAngularAleatoria(),
                    acelAngularInicial
                );
            }
        }
    }

    private ParticulasRotatorias BuscarInactivaRotatoria()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].gameObject.activeInHierarchy && pool[i] is ParticulasRotatorias pr)
                return pr;
        }
        return null;
    }

    private Vector3 VelAngularAleatoria()
    {
        return velAngularInicial + new Vector3(
            Random.Range(-velAngularVariacion, velAngularVariacion),
            Random.Range(-velAngularVariacion, velAngularVariacion),
            Random.Range(-velAngularVariacion, velAngularVariacion)
        );
    }
}