using UnityEngine;

// Representa un punto individual en la grid de agua
// Almacena su transform para modificar su altura
public class WavePoint : MonoBehaviour
{
    public Vector2 GridPosition { get; set; } // Posicion en la grid (X, Z)
    public float CurrentHeight { get; set; }  // Altura actual del punto

    private Transform pointTransform;
    private Vector3 basePosition;

    void Awake()
    {
        pointTransform = transform;
        basePosition = pointTransform.position;
    }

    // Actualiza la altura del punto
    public void UpdateHeight(float newHeight)
    {
        CurrentHeight = newHeight;
        pointTransform.position = new Vector3(
            basePosition.x,
            basePosition.y + newHeight,
            basePosition.z
        );
    }
}