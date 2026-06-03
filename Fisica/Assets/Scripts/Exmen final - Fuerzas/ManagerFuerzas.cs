using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Unity.VisualScripting;


public class ManagerFuerzas : MonoBehaviour
{
    // Colocar este script en un empty object

    public List<AplicadorFuerza> aplicadores;
    public List<Rigidbody> objetosAfectados;

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 0; i < aplicadores.Count; i++)
        {
            for(int j = 0; j < objetosAfectados.Count; j++)
            {
                float distanciaAlicadoresAfectados = Vector3.Distance(aplicadores[i].transform.position, objetosAfectados[j].transform.position);
                if (distanciaAlicadoresAfectados <= aplicadores[i].GetRadio() && aplicadores[i].aplicarFuerza)
                {
                    objetosAfectados[j].AddForce(aplicadores[i].CalcularFuerza(objetosAfectados[j]), ForceMode.Force);
                }
            }
        }
    }

    //void CalcularFuerzaGavedad()
    //{
    //    float F_gravedad = 0;
    //    float G = 9.81f;

    //    float m1 = 0;
    //    Vector3 obj1 = Vector3.zero;

    //    for (int i = 0; i < objetosAfectados.Count; i++) {
    //        m1 = objetosAfectados[i].mass;
    //        obj1 = objetosAfectados[i].position;
    //    }
        
    //    float m2 = 1;
        
    //    Vector3 obj2 = transform.position;
    //    float distancia = Vector3.Distance(obj1, obj2);
    //    F_gravedad = G * m1 * m2 * distancia;
    //}

    //public void CalcularFuerzaOnda()
    //{
    //    float t = Time.time;
    //    float frecuencia = 1 / t;
    //    float velocidad = 0;
    //    float intensidad = 0;
    //    float posZ = 0;
    //    float posX = 0;

    //    for (int i = 0; i < aplicadores.Count; i++)
    //    {
    //        velocidad = aplicadores[i].GetComponent<AplicadorOndas>().velocidad;
    //        intensidad = aplicadores[i].GetComponent<AplicadorOndas>().intensidad;
    //        posZ = aplicadores[i].transform.position.z;
    //        posX = aplicadores[i].transform.position.x;
    //    }

    //    float Fx = math.sin(t * velocidad + posZ * frecuencia) * intensidad;
    //    float Fz = math.sin(t * velocidad + posX * frecuencia) * intensidad;
    //}
}
