using UnityEngine;

public class Particulas : MonoBehaviour
{
    protected Vector3 vel;
    protected Vector3 acel;
    protected float lifeTime;
    protected float initialLifeTime;

    protected virtual void Start()
    {
        initialLifeTime = lifeTime;
    }

    void Update()
    {
        Resolve(Time.deltaTime);
    }

    public virtual void Resolve(float dt)
    {
        // Integración: velocidad += aceleración * dt, posición += velocidad * dt
        vel += acel * dt;
        transform.position += vel * dt;

        lifeTime -= dt;

        if (lifeTime <= 0f)
            gameObject.SetActive(false);
    }

    // Inicializa la partícula con los datos que le pasa el Emisor
    public virtual void Init(Vector3 position, Vector3 velocidad, Vector3 aceleracion, float vida)
    {
        transform.position = position;
        vel = velocidad;
        acel = aceleracion;
        lifeTime = vida;
        initialLifeTime = vida;
    }

    public virtual void ResetParticle(Vector3 position, Vector3 velocidad, Vector3 aceleracion, float vida)
    {
        transform.position = position;
        vel = velocidad;
        acel = aceleracion;
        lifeTime = vida;
    }
}