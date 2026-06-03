using Unity.VisualScripting;
using UnityEngine;

public class AplocadorGravedadPuntual : AplicadorFuerza
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    public override Vector3 CalcularFuerza(Rigidbody rb)
    {
        float F_gravedad = 0;
        float G = 9.81f;

        float m1 = rb.mass;
        float m2 = 1;

        Vector3 obj1 = rb.position;
        Vector3 obj2 = transform.position;

        float distancia = Vector3.Distance(obj1, obj2);

        Vector3 direccion = (obj2 - obj1).normalized;

        F_gravedad = G * m1 * m2 * distancia;

        return direccion * F_gravedad;
    }

    //public float CalcularFuerzaGavedad()
    //{
    //    float F_gravedad = 0;
    //    float G = 9.81f;

    //    float m1 = 0;
    //    Vector3 obj1 = Vector3.zero;

    //    for (int i = 0; i < objetosAfectados.Count; i++)
    //    {
    //        m1 = objetosAfectados[i].mass;
    //        obj1 = objetosAfectados[i].position;
    //    }

    //    float m2 = 1;

    //    Vector3 obj2 = transform.position;
    //    float distancia = Vector3.Distance(obj1, obj2);
    //    F_gravedad = G * m1 * m2 * distancia;
    //}
}
