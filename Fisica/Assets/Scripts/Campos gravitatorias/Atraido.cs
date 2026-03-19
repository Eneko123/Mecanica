using System.Collections.Generic;
using UnityEngine;

public class Atraido : MonoBehaviour
{
    public List<Atractor> atractores;
    private Rigidbody rb;

    public float velocidadGiro = 5f;

    // Cache de datos calculados cada FixedUpdate
    private float[] distancias;
    private float[] fuerzas;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false; // Usamos nuestra propia gravedad

        distancias = new float[atractores.Count];
        fuerzas    = new float[atractores.Count];
    }

    private void FixedUpdate()
    {
        Atraccion();
    }

    private void Update()
    {
        Mirar();
    }

    private void Atraccion()
    {
        for (int i = 0; i < atractores.Count; i++)
        {
            Vector3 direccion = atractores[i].transform.position - transform.position;
            distancias[i] = direccion.magnitude;

            if (distancias[i] > atractores[i].radio || distancias[i] == 0f)
            {
                fuerzas[i] = 0f;
                continue;
            }

            // F = G * (M * m) / d²
            fuerzas[i] = 9.8f * (atractores[i].masa * rb.mass) / (distancias[i] * distancias[i]);

            rb.AddForce(direccion.normalized * fuerzas[i], ForceMode.Force);
        }
    }

    private void Mirar()
    {
        Atractor planetaDominante = ObtenerPlanetaDominante();
        if (planetaDominante == null) return;

        Vector3 dirHaciaArriba = (transform.position - planetaDominante.transform.position).normalized;

        Quaternion targetRot = Quaternion.FromToRotation(transform.up, dirHaciaArriba) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * velocidadGiro);
    }

    private Atractor ObtenerPlanetaDominante()
    {
        Atractor dominante = null;
        float maxFuerza = 0f;

        for (int i = 0; i < fuerzas.Length; i++)
        {
            if (fuerzas[i] > maxFuerza)
            {
                maxFuerza = fuerzas[i];
                dominante = atractores[i];
            }
        }

        return dominante;
    }
}