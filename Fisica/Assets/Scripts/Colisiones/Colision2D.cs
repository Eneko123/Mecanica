using UnityEngine;

public class Colision2D : MonoBehaviour
{
    public Transform colliderCenter;
    public float colliderHeght;
    public float colliderWidth;
    public Material mat;
    public Color collidingColor;
    public Color freeColor;
    public Transform point;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3.forward;
        //Vector3.up;
        if (!ColisionWith2D())
        {
            mat.color = collidingColor;
        }
        else
        {
            mat.color = freeColor;
        }
    }

    bool ColisionWith2D()
    {
        float distanceX = +(colliderCenter.position.x - point.position.x);
        float distanceY = +(colliderCenter.position.y - point.position.y);

        bool touchX = distanceX < colliderHeght;
        bool touchY = distanceY < colliderWidth;

        return touchX && touchY;
    }
}
