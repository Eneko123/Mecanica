using UnityEngine;

public class Caja : MonoBehaviour
{
    public bool entregada { get; private set; } = false;
    private Vector3 startPos;
    private Quaternion startRot;
    private Rigidbody rb;

    void Start()
    {
        startPos = transform.position;
        startRot = transform.rotation;
        rb = GetComponent<Rigidbody>();
    }

    public void ResetCaja()
    {
        transform.position = startPos;
        transform.rotation = startRot;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        entregada = false;
    }

    public void MarcarEntregada() => entregada = true;
}