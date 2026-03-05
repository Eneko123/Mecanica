using UnityEngine;

public class Muelle : MonoBehaviour
{
    public float K;
    public float longitudMuelle;
    public float amortiguacion;
    public float rayCastDistance = 10f;
    public LayerMask layerMask;

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
            if(hit.distance > rayCastDistance) return; // Si el raycast no alcanza el suelo, no hacer nada
            float elongacion = hit.distance - longitudMuelle;
            float fuerzaMuelle = -K * elongacion - (rb.linearVelocity.y - amortiguacion);
            if (fuerzaMuelle < 0) fuerzaMuelle = 0; // Evitar que el muelle tire hacia abajo
            rb.AddForce(fuerzaMuelle * Vector3.up, ForceMode.Force);
        }
    }
}
