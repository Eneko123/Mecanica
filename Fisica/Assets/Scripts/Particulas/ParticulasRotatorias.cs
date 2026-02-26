using UnityEngine;

public class ParticulasRotatorias : Particulas
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = new Vector3(
            5f,
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f)
        );

        vel = new Vector3(
            Random.Range(-3f, 3f),
            Random.Range(-3f, 3f),
            5f
        );

        initialLifeTime = lifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public new void ResetParticle()
    {
        lifeTime = initialLifeTime;
        transform.position = new Vector3(
            5f,
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f)
        );
    }
}
