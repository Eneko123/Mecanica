using UnityEngine;

public class MovAngular : MonoBehaviour
{
    Vector3 angularVelocity;
    Vector3 angularAcceleration;

    float accelerationStrength = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        angularAcceleration = Vector3.zero;
        if (Input.GetKey(KeyCode.A))
        {
            angularAcceleration.x += accelerationStrength;
        }
        if (Input.GetKey(KeyCode.D))
        {
            angularAcceleration.x -= accelerationStrength;
        }
        Integrar(Time.deltaTime);
    }

    void Integrar(float dt)
    {         
        angularVelocity += angularAcceleration * dt;
        transform.Rotate(angularVelocity * dt);
    }
}
