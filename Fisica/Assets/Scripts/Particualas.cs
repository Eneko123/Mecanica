using UnityEngine;

public class Particualas : MonoBehaviour
{
    [SerializeField] public Vector3 vel;
    [SerializeField] float mass;
    [SerializeField] Vector3 forceAcum;
    [SerializeField] public float rad = 1;


    void Update()
    {
        Resolve(Time.deltaTime);
    }

    public void Resolve(float dt)
    {
        Vector3 p = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        p = p + vel;

        // F = m * a => a = F / m Vector2 a = force / mass; vel = vel + a * dt;
        Vector3 a = forceAcum / mass;

        // velocidad iguala a la velocidad anterior mas la aceleracion por el tiempo
        vel += a * dt;


        transform.position = new Vector3(p.x, p.y, 0);
        forceAcum = Vector3.zero;
    }
}
