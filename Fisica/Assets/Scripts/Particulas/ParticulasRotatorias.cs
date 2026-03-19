using UnityEngine;

public class ParticulasRotatorias : Particulas
{
    // Ángulos de rotación (Roll = Z, Pitch = X, Yaw = Y)
    private Vector3 rotacion;       // Roll, Pitch, Yaw en grados
    private Vector3 velAngular;     // Velocidad angular (grados/s)
    private Vector3 acelAngular;    // Aceleración angular (grados/s²)

    private float masa = 1f;

    // Inercia: I = 2 * masa / 5
    private float Inercia => 2f * masa / 5f;

    public void InitRotacion(Vector3 position, Vector3 velocidad, Vector3 aceleracion, float vida, float m, Vector3 velAng, Vector3 acelAng)
    {
        masa = m;
        velAngular = velAng;
        acelAngular = acelAng;
        rotacion = Vector3.zero;
        base.Init(position, velocidad, aceleracion, vida);
    }

    public override void Resolve(float dt)
    {
        // Actualiza posición (hereda de Particulas)
        base.Resolve(dt);

        // alpha_efectiva = torque / I
        // Aquí tratamos acelAngular como torque angular
        Vector3 alphaEfectiva = acelAngular / Inercia;
        velAngular += alphaEfectiva * dt;
        rotacion += velAngular * dt;

        transform.rotation = Quaternion.Euler(rotacion.x, rotacion.y, rotacion.z);
    }

    public override void ResetParticle(Vector3 position, Vector3 velocidad, Vector3 aceleracion, float vida)
    {
        base.ResetParticle(position, velocidad, aceleracion, vida);
        rotacion = Vector3.zero;
    }
}