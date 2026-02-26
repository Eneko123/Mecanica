using UnityEngine;

public class Particulas : MonoBehaviour
{
    private Vector3 vel;
    private float lifeTime = 1f;
    private float initialLifeTime;

    void Start()
    {
        transform.position = new Vector3(
            0f,
            Random.Range(-2f, 2f),
            Random.Range(-2f, 2f)
        );

        vel = new Vector3(
            Random.Range(-5f, 5f),
            Random.Range(-5f, 5f),
            5f
        );

        initialLifeTime = lifeTime;
    }

    void Update()
    {
        Resolve(Time.deltaTime);
    }

    public void Resolve(float dt)
    {
        // Mueve la particula
        transform.position += vel * dt;

        // Reduce vida
        lifeTime -= dt;

        // Destruye cuando muere
        if (lifeTime <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void ResetParticle()
    {
        lifeTime = initialLifeTime;
        transform.position = new Vector3(
            Random.Range(-2f, 2f),
            Random.Range(-2f, 2f),
            0f
        );
    }
}
