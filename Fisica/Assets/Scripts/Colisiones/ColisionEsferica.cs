using UnityEngine;

public class ColisionEsferica : MonoBehaviour
{
    public Transform colliderCenter;
    public float radius;
    public Transform point;
    public Material mat;
    public Color collidingColor;
    public Color freeColor;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (!CalculateEsfereRadius())
        {
            mat.color = collidingColor;
        }
        else
        {
            mat.color = freeColor;
        }
    }

    bool CalculateEsfereRadius() 
    {
        return radius < (Vector3.Distance(colliderCenter.position, point.position));
    }
}
