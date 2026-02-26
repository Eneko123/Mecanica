using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Atraido : MonoBehaviour
{
    public List<GameObject> atractores;

    public GameObject atraido;
    public Rigidbody rbAtraido;

    public float[] distancias;

    public float[] fuerzas;

    private float radio = 50;


    void Start()
    {
        for (int i = 0; i < atractores.Count; i++)
        {
            atractores[i].GetComponent<Rigidbody>();
        }
        distancias = new float[atractores.Count];
        fuerzas = new float[atractores.Count];
    }

    void Update()
    {
        Atraccion();
        Mirar();
    }

    void Atraccion()
    {
        for (int i = 0; i < atractores.Count; i++)
        {
            distancias[i] = Vector3.Distance(atractores[i].GetComponent<Rigidbody>().position, rbAtraido.position);

            fuerzas[i] = 9.8f * ((atractores[i].GetComponent<Rigidbody>().mass * rbAtraido.mass) / (distancias[i] * distancias[i]));
            if (distancias[i] > radio)
            {
                fuerzas[i] = 0;
            }

            atraido.GetComponent<Rigidbody>().AddForce(new Vector3(
                atractores[i].transform.position.x - atraido.transform.position.x,
                atractores[i].transform.position.y - atraido.transform.position.y,
                atractores[i].transform.position.z - atraido.transform.position.z) *
                fuerzas[i],
                ForceMode.Force);
        }
    }

    void Mirar()
    {
        for (int i = 0; i < atractores.Count; i++)
        {
            atraido.transform.LookAt(atractores[i].transform);
        }
    }
}
