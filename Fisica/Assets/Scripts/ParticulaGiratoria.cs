using UnityEngine;

public class ParticulaGiratoria : Particulas
{
    public Quaternion transformRotation;
    public Vector3 angulosEuler; // Roll, Pitch, Yaw
    public Vector3 velocidadAngular;
    public Vector3 aceleracionAngular;

    // Inercia (PDF: I = 2 * masa / 5)
    public float masa = 1.0f;
    public float Inercia { get { return (2 * masa) / 5; } }

    public void InicializarRotacion(Vector3 velAng, Vector3 accAng)
    {
        velocidadAngular = velAng;
        aceleracionAngular = accAng;
        angulosEuler = Vector3.zero;
    }

    public override void Resolve(float deltaTime)
    {
        base.Resolve(deltaTime); // Actualizar posicion normal

        if (activa)
        {
            // Actualizacion de la rotacion 
            // Nota: La inercia afecta como cambia la velocidad si hay Torque.
            // Si solo hay velocidad angular constante:
            velocidadAngular += aceleracionAngular * deltaTime;
            angulosEuler += velocidadAngular * deltaTime;

            // Aplicar rotacion al grafico
            transformRotation = Quaternion.Euler(angulosEuler);
        }
    }
}