using UnityEngine;

public class Particulas : MonoBehaviour
{
    public Transform pos;
    public Vector3 vel = new Vector3 (1, 1, 2);
    public float lifeTime = 3;

    void Start()
    {
        pos (Random.Range(-2f, 2f), Random.Range(-2f, 2f), 0f);
    }

    void Update()
    {
        Resolve(Time.deltaTime);
    }

    public void Resolve(float dt)
    {
        Vector3 p = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        p += vel;

        lifeTime -= dt;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
