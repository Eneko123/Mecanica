using UnityEngine;

public class AplicadorFreno : AplicadorFuerza
{
    public float intensidadFrenado = 0;
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
        Vector3 fuerzaFrenado = Vector3.zero;

        float velocidadEnDireccion = Vector3.Dot(rb.position, rb.linearVelocity.normalized);

        Vector3 obj1 = rb.position;
        Vector3 obj2 = transform.position;

        Vector3 direccion = (obj2 - obj1).normalized;

        fuerzaFrenado = -direccion * velocidadEnDireccion * intensidadFrenado;

        return fuerzaFrenado;
    }
}
