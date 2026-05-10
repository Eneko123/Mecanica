using UnityEngine;

public class ZonaEntrega : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Caja caja = other.GetComponent<Caja>();
        if (caja != null && !caja.entregada)
        {
            caja.MarcarEntregada();
            GameManager.Instance.RegistrarEntrega();
        }
    }
}