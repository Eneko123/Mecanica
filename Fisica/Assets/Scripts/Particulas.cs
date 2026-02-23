using UnityEngine;

public class Particulas : MonoBehaviour
{
    public Vector3 posicion;
    public Vector3 velocidad;
    public Vector3 aceleracion;
    public float tiempoDeVida;
    public float tiempoDeVidaMax;
    public bool activa;

    public void Inicializar(Vector3 pos, Vector3 vel, float vida)
    {
        posicion = pos;
        velocidad = vel;
        aceleracion = Vector3.zero; // O gravedad
        tiempoDeVida = vida;
        tiempoDeVidaMax = vida;
        activa = true;
    }

    public void Update()
    {
        Resolve(Time.deltaTime);
    }

    public virtual void Resolve(float dt)
    {
        if (!activa) return;

        // Integración de movimiento (Euler simple)
        velocidad += aceleracion * dt;
        posicion += velocidad * dt;

        // Reducir vida útil
        tiempoDeVida -= dt;

        if (tiempoDeVida <= 0)
        {
            activa = false; // Marcar para eliminar
        }
    }
}

