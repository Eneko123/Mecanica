using UnityEngine;

public class Barco : MonoBehaviour
{
    public float rayCastDistance = 10f;
    public LayerMask layerMask;
    public GameObject p1;
    public GameObject p2;
    public GameObject p3;

    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        Vector3 origen = this.transform.position;
        
            if (Physics.Raycast(origen, Vector3.down, out RaycastHit hit, rayCastDistance, layerMask))
            {
                if (hit.distance > rayCastDistance) return; // Si el raycast no alcanza el suelo, no hacer nada
                float distancia = hit.distance - rayCastDistance;
              
                rb.AddTorque(Vector3.up, ForceMode.Force);
            }
        
    }

    void CrearPlano()
    {
        Vector3 vector1 = p1.transform.position - p2.transform.position;
        Vector3 vector2 = p1.transform.position - p3.transform.position;
        Vector3 normal = Vector3.Cross(vector1, vector2).normalized;

    }
}
