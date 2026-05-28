using System;
using Unity.VisualScripting;
using UnityEngine;

public class MuelleEntreObjetos : MonoBehaviour
{
    public Transform PuntoA;
    public Transform PuntoB;

    public Rigidbody rbA;
    public Rigidbody rbB;

    public float longitudReposo = 2;
    public float rigidez = 50;
    public float amortiguamiento = 5;

    private float energia;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(PuntoA.name + " y " + PuntoB.name + " tiene una energia potencial de: " + energia);
    }

    private void FixedUpdate()
    {
        Vector3 velocidadA;
        if (rbA != null)
        {
            velocidadA = rbA.linearVelocity;
        }
        else
        {
            velocidadA = Vector3.zero;
        }
        Vector3 velocidadB = rbB.linearVelocity;
        Vector3 V_relativa = velocidadB - velocidadA;

        Vector3 direccionActual = PuntoA.position - PuntoB.position;

        float V_muelle = Vector3.Dot(V_relativa, direccionActual.normalized);

        float distanciaActual = (PuntoA.position - PuntoB.position).magnitude;
        float x = distanciaActual - longitudReposo;
        float k = rigidez;
        float F_muelle = k * x;

        float c = amortiguamiento;
        float F_total = F_muelle - c * V_muelle;

        Vector3 direccionA = (PuntoB.position - PuntoA.position).normalized;


        if (rbA  != null)
        {

            rbA.AddForce(direccionA * F_total);
        }

        rbB.AddForce(-direccionA * F_total);

        energia = CalcularEnergia(k, x);
    }

    float CalcularEnergia(float k, float x)
    {
        float E_e = 1/2.0f * k * x * x;

        return E_e;
    }
}
