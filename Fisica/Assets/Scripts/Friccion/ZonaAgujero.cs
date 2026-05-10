using UnityEngine;

public class ZonaAgujero : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Si una caja toca el agujero
        if (other.CompareTag("Caja"))
        {
            Debug.Log("¡Una caja cayo al agujero! Reiniciando posiciones...");

            if (GameManager.Instance != null)
            {
                // Reinicia cajas
                GameManager.Instance.ReiniciarCajas();
            }
        }
    }
}
