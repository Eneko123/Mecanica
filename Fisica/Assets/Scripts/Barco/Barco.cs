using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Barco : MonoBehaviour
{
    [Header("Puntos")]
    public GameObject p1;
    public GameObject p2;
    public GameObject p3;

    [Header("Configuracion de Raycast")]
    // Distancia del raycast 
    public float rayCastDistance = 10f;
    // Layer de la superficie sobre la que flotar
    public LayerMask layerMask;

    [Header("Parametros del Muelle Vertical")]
    public float k = 50f;
    public float c = 5f;
    public float offsetFlotacion = 1.5f;

    [Header("Parametros del Muelle Angular")]
    // Constante angular
    public float kAngular = 30f;
    // Coeficiente de amortiguacion angular
    public float dAngular = 3f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // flotacion vertical
        AplicarFlotacionVertical();

        // rotacion mediante muelles
        AplicarRotacionAngular();
    }

    void AplicarFlotacionVertical()
    {
        int puntosDetectados = 0;
        float sumaAlturasAgua = 0f;

        // Raycast desde p1
        if (Physics.Raycast(p1.transform.position, Vector3.down, out RaycastHit hit1, rayCastDistance, layerMask))
        {
            sumaAlturasAgua += hit1.point.y;
            puntosDetectados++;
        }

        // Raycast desde p2
        if (Physics.Raycast(p2.transform.position, Vector3.down, out RaycastHit hit2, rayCastDistance, layerMask))
        {
            sumaAlturasAgua += hit2.point.y;
            puntosDetectados++;
        }

        // Raycast desde p3
        if (Physics.Raycast(p3.transform.position, Vector3.down, out RaycastHit hit3, rayCastDistance, layerMask))
        {
            sumaAlturasAgua += hit3.point.y;
            puntosDetectados++;
        }

        if (puntosDetectados == 0) return;

        // Altura media de la superficie detectada
        float alturaMediaSuperficie = sumaAlturasAgua / puntosDetectados;

        // Altura de flotacion
        float alturaEquilibrio = alturaMediaSuperficie + offsetFlotacion;

        // Altura actual del objeto
        float alturaObjeto = transform.position.y;

        // Desplazamiento
        float desplazamiento = alturaEquilibrio - alturaObjeto;

        // Fuerzas
        float fuerzaElastica = k * desplazamiento;
        float velocidadVertical = rb.linearVelocity.y;
        float fuerzaAmortiguacion = -c * velocidadVertical;
        float fuerzaTotal = fuerzaElastica + fuerzaAmortiguacion;

        rb.AddForce(Vector3.up * fuerzaTotal, ForceMode.Force);
    }

    void AplicarRotacionAngular()
    {
        // Verificar que los tres puntos tienen hits válidos
        if (!LanzarRaycast(p1.transform.position, out float hit1)) return;
        if (!LanzarRaycast(p2.transform.position, out float hit2)) return;
        if (!LanzarRaycast(p3.transform.position, out float hit3)) return;

        // Calcular el plano definido por los tres puntos de impacto
        Vector3 puntoImpacto1 = new Vector3(p1.transform.position.x, hit1, p1.transform.position.z);
        Vector3 puntoImpacto2 = new Vector3(p2.transform.position.x, hit2, p2.transform.position.z);
        Vector3 puntoImpacto3 = new Vector3(p3.transform.position.x, hit3, p3.transform.position.z);

        // Obtener el vector normal objetivo del plano
        Vector3 normalObjetivo = CalcularNormalPlano(puntoImpacto1, puntoImpacto2, puntoImpacto3);

        // Comparar con transform.up del objeto
        Vector3 transformUp = transform.up;

        // Calcular el ángulo de error entre transform.up y normalObjetivo
        float anguloError = Vector3.Angle(transformUp, normalObjetivo) * Mathf.Deg2Rad;

        // Calcular el eje de rotación (producto cruzado)
        Vector3 eje = Vector3.Cross(transformUp, normalObjetivo);

        // Si el eje es muy pequenio, las normales son casi iguales, no hacer nada
        if (eje.magnitude < 0.001f) return;

        eje.Normalize();

        // Calculo del torque
        // Modelo angular
        float fuerzaAngular = kAngular * anguloError;

        // Velocidad angular del rigidbody proyectada sobre el eje
        float velocidadAngular = Vector3.Dot(rb.angularVelocity, eje);
        float amortiguacionAngular = -dAngular * velocidadAngular;

        // Torque total
        float torqueTotal = fuerzaAngular + amortiguacionAngular;

        // Aplicar torque en el eje calculado
        rb.AddTorque(eje * torqueTotal, ForceMode.Force);
    }

    // Lanza un raycast vertical hacia abajo desde una posición y devuelve la altura del impacto
    bool LanzarRaycast(Vector3 origen, out float alturaImpacto)
    {
        alturaImpacto = 0f;

        if (Physics.Raycast(origen, Vector3.down, out RaycastHit hit, rayCastDistance, layerMask))
        {
            alturaImpacto = hit.point.y;
            return true;
        }

        return false;
    }

    // Calcula la normal de un plano definido por tres puntos
    Vector3 CalcularNormalPlano(Vector3 punto1, Vector3 punto2, Vector3 punto3)
    {
        // Dos vectores en el plano
        Vector3 vector1 = punto2 - punto1;
        Vector3 vector2 = punto3 - punto1;

        // Producto cruzado da la normal
        Vector3 normal = Vector3.Cross(vector1, vector2).normalized;

        // Asegurarnos de que la normal apunta hacia arriba
        if (normal.y < 0)
        {
            normal = -normal;
        }

        return normal;
    }

    // Visualización en el editor
    void OnDrawGizmos()
    {
        if (p1 != null && p2 != null && p3 != null)
        {
            // Dibujar raycasts
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(p1.transform.position, Vector3.down * rayCastDistance);
            Gizmos.DrawRay(p2.transform.position, Vector3.down * rayCastDistance);
            Gizmos.DrawRay(p3.transform.position, Vector3.down * rayCastDistance);
        }
    }
}