using UnityEngine;

public class Muelle2 : MonoBehaviour
{
    float amplitud = 10f; // Amplitud del movimiento
    float vAngualr = Mathf.PI; // Velocidad angular del movimiento

    //Transform posicion;
    //Vector3 posicionInicial; // Posición inicial del muelle
    //Vector3 posicionMaxima = new Vector3(0, 10, 25);

    //float velocidad = 1f; // Velocidad del movimiento


    //float frecuencia = 1f; // Frecuencia del movimiento

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //posicion = this.transform;
        //posicionInicial = posicion.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movimiento();
    }

    void Movimiento()
    {
        //amplitud = Vector3.Distance(posicionInicial, posicionMaxima);
        //float tiempo = Time.deltaTime * frecuencia;
        //posicion.position = posicionInicial + Vector3.up * Mathf.Sin(tiempo) * amplitud;
        float newY = amplitud * Mathf.Sin(Time.time * vAngualr);
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
