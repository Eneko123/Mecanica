using UnityEngine;

public class Torque : MonoBehaviour
{
    public Transform centro;
    public Transform troqueador;
    public float fuerzaTorque = 10f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Torcar();
    }

    void Torcar()
    {
        Vector3 direccion = centro.position - troqueador.position; // Vector desde el objeto hacia el centro
        // Vector3 magnitud = direccion.normalized; // Normalizar el vector para obtener solo la dirección
        Vector3 torque = Vector3.Cross(direccion, Vector3.up); // Calcular el torque usando el producto cruzado
        GetComponent<Rigidbody>().AddTorque(torque * fuerzaTorque); // Aplicar el torque al Rigidbody del objeto
    }
}
