using System.Collections.Generic;
using UnityEngine;

public class Emisor : MonoBehaviour
{
    [SerializeField] public List<Particulas> particualas = new List<Particulas>();
    private float accumTime = 1f;
    private float genTime = 1f;

    void Update()
    {
        float dt = Time.time;
        accumTime += dt;

        while (accumTime >= genTime)
        {
            // Generate a new particle
            accumTime -= genTime;
            Particulas newParticle = Instantiate(particualas[0], transform.position, Quaternion.identity);
        }
        // add force

        // Update existing particles
        // destroy particles that are out of bounds or have expired with TTL = 0;
    }
}
