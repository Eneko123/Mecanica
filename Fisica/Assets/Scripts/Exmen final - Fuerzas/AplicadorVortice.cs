using UnityEngine;

public class AplicadorVortice : AplicadorFuerza
{
    public float fuerzaTangencial = 0;
    public float fuerzaVortice = 0;
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

        Vector3 obj1 = rb.position;
        Vector3 obj2 = transform.position;

        Vector3 direccion = (obj2 - obj1).normalized;
        Vector3 direccionVortice = (obj1 - obj2).normalized;
        Vector3 direccionTangencial = Vector3.Cross(direccionVortice, direccion).normalized;
        

        return direccion + direccionTangencial * fuerzaVortice * fuerzaTangencial;
    }
}
