using UnityEngine;

public class ColisionAABBPoint : MonoBehaviour
{
    public Transform colliderCenter;
    public float colliderHeght;
    public float colliderWidth;
    public float colliderLenght;
    public Material mat;
    public Color collidingColor;
    public Color freeColor;
    public Transform point;
    public MeshFilter filter;

    private void Start()
    {

    }

    private void Update()
    {
        //filter.mesh.bounds.;
        if (!CalculateAABBCol())
        {
            mat.color = collidingColor;
        }
        else
        {
            mat.color = freeColor;
        }
    }

    bool CalculateAABBCol()
    {
        float minPointX = colliderCenter.position.x - colliderWidth * 0.5f;
        float maxPointX = colliderCenter.position.x + colliderWidth * 0.5f;
        float minPointY = colliderCenter.position.y - colliderHeght * 0.5f;
        float maxPointY = colliderCenter.position.y + colliderHeght * 0.5f;
        float minPointZ = colliderCenter.position.z - colliderLenght * 0.5f;
        float maxPointZ = colliderCenter.position.z + colliderLenght * 0.5f;

        bool axisX = point.position.x > minPointX && point.position.x < maxPointX;
        bool axisY = point.position.y > minPointY && point.position.y < maxPointY;
        bool axisZ = point.position.z > minPointY && point.position.z < maxPointY;

        return axisX && axisY && axisZ;
    }
}
