using UnityEngine;

public class ColisionesOBBCuadradoCirculo : MonoBehaviour
{
    [SerializeField] private Transform cube;
    [SerializeField] private Transform circle;
    private float cicleRadius;

    private Vector2 cubeFront;
    private Vector2 cubeUp;

    private Vector2 cubeXL;
    private Vector2 cubeXR;
    private Vector2 cubeYL;
    private Vector2 cubeYR;

    // Update is called once per frame
    void Update()
    {
        
    }

    // Funcion que calcula los vertices de ambos objetos y los vectores front y up de cada objeto
    void CalcualtePointsAndRadious()
    {
        cicleRadius = circle.localScale.x / 2;

        cubeFront = Vector2.zero + new Vector2(cube.right.x, 0);
        cubeUp = Vector2.zero + new Vector2(0, cube.up.y);

        cubeXL = -cubeFront * (cube.localScale.x / 2) + (Vector2)cube.position;
        cubeXR = cubeFront * (cube.localScale.x / 2) + (Vector2)cube.position;
        cubeYL = -cubeUp * (cube.localScale.y / 2) + (Vector2)cube.position;
        cubeYR = cubeUp * (cube.localScale.y / 2) + (Vector2)cube.position;
    }  

    float CalculateDistance(Vector2 point1, Vector2 point2)
    {
        return Vector2.Distance(point1, point2);
    }
}
