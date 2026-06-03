using UnityEngine;

public class AplicadorFuerza : MonoBehaviour
{
    public bool aplicarFuerza = false;
    [SerializeField] private float radio = 0;

    public virtual Vector3 CalcularFuerza(Rigidbody rb)
    {
        return Vector3.zero;
    }

    public float GetRadio() { return radio; }
    public void SetRadio(float newRadio) { radio = newRadio; }
}
