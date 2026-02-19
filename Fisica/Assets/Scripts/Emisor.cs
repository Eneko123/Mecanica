using System.Collections.Generic;
using UnityEngine;

public class Emisor : MonoBehaviour
{
    [SerializeField] public List<Particualas> particualas = new List<Particualas>();
    public float accumTime = 0f;
    public float genTime = 0.001f;

    void Update()
    {
        float dt = Time.time;
        accumTime += dt;

        while (accumTime >= genTime)
        {
            // Generate a new particle
            accumTime -= genTime;
            Particualas newParticle = Instantiate(particualas[0], transform.position, Quaternion.identity);
            newParticle.vel = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
        }
    }
}
