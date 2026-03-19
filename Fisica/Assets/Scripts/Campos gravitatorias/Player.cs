using UnityEngine;

public class Player : MonoBehaviour
{
    public float velocidad = 8f;
    public float fuerzaSalto = 300f;
    public float velocidadGiro = 5f;

    public Atraido atraido; 

    private Rigidbody rb;
    private bool enSuelo = false;

    // Para detectar colisión con atractores
    private int contactosActivos = 0;

    public ParticleSystem particulasSalto;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.freezeRotation = true;
    }

    private void Update()
    {
        GestionarSalto();
    }

    private void FixedUpdate()
    {
        GestionarMovimiento();
    }

    private void GestionarMovimiento()
    {
        float inputH = Input.GetAxis("Horizontal");
        float inputV = Input.GetAxis("Vertical");   

        // Vector "adelante" y "derecha" del jugador proyectados sobre la superficie
        // Asi el movimiento respeta el plano tangente al planeta
        Vector3 adelante = Vector3.ProjectOnPlane(transform.forward, transform.up).normalized;
        Vector3 derecha = Vector3.ProjectOnPlane(transform.right, transform.up).normalized;

        Vector3 movimiento = (adelante * inputV + derecha * inputH) * velocidad;

        // Aplicamos solo la parte horizontal
        rb.AddForce(movimiento, ForceMode.Force);
    }

    private void GestionarSalto()
    {
        // Solo salta si esta en contacto con un atractor
        if (Input.GetKeyDown(KeyCode.Space) && enSuelo)
        {
            rb.AddForce(transform.up * fuerzaSalto, ForceMode.Impulse);
            enSuelo = false;

            if (particulasSalto != null)
            {
                particulasSalto.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                particulasSalto.Play();
            }
        }
    }

    // Detectar si esta pisando un atractor
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Atractor>() != null)
        {
            contactosActivos++;
            enSuelo = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<Atractor>() != null)
        {
            contactosActivos--;
            if (contactosActivos <= 0)
            {
                contactosActivos = 0;
                enSuelo = false;
            }
        }
    }
}
