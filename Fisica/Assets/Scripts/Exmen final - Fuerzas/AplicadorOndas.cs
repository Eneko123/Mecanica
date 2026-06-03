using Unity.Mathematics;
using UnityEngine;

public class AplicadorOndas : AplicadorFuerza
{
    public float intensidad = 0;
    public float velocidad = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override Vector3 CalcularFuerza(Rigidbody rb)
    {
        float Fx = 0;
        float Fz = 0;

        float t = Time.time;
        float frecuencia = 1 / t;

        float posZ = rb.transform.position.z;
        float posX = rb.transform.position.x;

        Vector3 obj1 = rb.position;
        Vector3 obj2 = transform.position;

        Vector3 direccion = (obj2 - obj1).normalized;
        if (t != 0)
        {
            Fx = math.sin(t * velocidad + posZ * frecuencia) * intensidad;
            Fz = math.cos(t * velocidad + posX * frecuencia) * intensidad;
        }
        else
        {
             Fx = 0;
             Fz = 0;
        }

        return direccion * Fx * Fz;
    }

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
