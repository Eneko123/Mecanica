using UnityEngine;
using System.Collections.Generic;

public class EmisorParticulas : MonoBehaviour
{
    [Header("Configuración del Emisor")]
    public float ratioEmision = 10f; // Particulas por segundo
    public float vidaInicial = 2f;
    public Vector3 velocidadInicial = Vector3.up;

    // Referencia al sistema que gestiona las particulas
    public SistemaParticulas sistema;

    private float acumuladorTiempo = 0f;

    void Update()
    {
        // Emitir partículas si es necesario
        acumuladorTiempo += Time.deltaTime;
        float intervalo = 1f / ratioEmision;

        if (acumuladorTiempo >= intervalo)
        {
            Emitir();
            acumuladorTiempo -= intervalo;
        }
    }

    void Emitir()
    {
        // Solicitar al sistema una particula disponible
        sistema.SolicitarParticula(transform.position, velocidadInicial, vidaInicial);
    }
}