using UnityEngine;

public class Force : MonoBehaviour
{
    [SerializeField] public Vector2 vel;
    [SerializeField] float mass;
    [SerializeField] Vector2 forceAcum;
    [SerializeField] public float rad = 1;


    void Update()
    {
        // Resolve(Time.deltaTime);
    }

    public void Resolve(float dt)
    {
        Vector2 p = new Vector2(transform.position.x, transform.position.y);
        p = p + vel;

        // F = m * a => a = F / m Vector2 a = force / mass; vel = vel + a * dt;
        Vector2 a = forceAcum / mass;

        // velocidad iguala a la velocidad anterior mas la aceleracion por el tiempo
        vel += a * dt;


        transform.position = new Vector3(p.x, p.y, 0);
        forceAcum = Vector2.zero;
    }
}
