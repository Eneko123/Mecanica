using Unity.VisualScripting;
using UnityEngine;

public class ColisionesOBB : MonoBehaviour
{
    [SerializeField] private Transform object1;
    [SerializeField] private Transform object2;

    [SerializeField]private Vector2 object1Front;
    [SerializeField]private Vector2 object1Up;

    [SerializeField]private Vector2 object2Front;
    [SerializeField]private Vector2 object2Up;

    [SerializeField]private Vector2 o1UL;
    [SerializeField]private Vector2 o1UR;
    [SerializeField]private Vector2 o1DL;
    [SerializeField]private Vector2 o1DR;

    [SerializeField]private Vector2 o2UL;
    [SerializeField]private Vector2 o2UR;
    [SerializeField]private Vector2 o2DL;
    [SerializeField]private Vector2 o2DR;

    [SerializeField]private float object1HalfWidth;
    [SerializeField]private float object1HalfHeight;

    [SerializeField]private float object2HalfWidth;
    [SerializeField] private float object2HalfHeight;

    // Update is called once per frame
    void Update()
    {
        CalculatePointsAndRadius();
        if (ColisionEje(object1Front) && ColisionEje(object1Up) && ColisionEje(object2Front) && ColisionEje(object2Up))
        {
            Debug.Log("Colision");
        }
    }

    // Funcion que calcula los vertices de ambos objetos y los vectores front y up de cada objeto
    void CalculatePointsAndRadius()
    {
        object1Front = new Vector2(object1.right.x, object1.right.y);
        object1Up = new Vector2(object1.up.x, object1.up.y);

        object2Front = new Vector2(object2.right.x, object2.right.y);
        object2Up = new Vector2(object2.up.x, object2.up.y);

        object1HalfWidth = object1.localScale.x / 2;
        object1HalfHeight = object1.localScale.y / 2;

        object2HalfWidth = object2.localScale.x / 2;
        object2HalfHeight = object2.localScale.y / 2;

        o1UL = -object1Front * object1HalfWidth + object1Up * object1HalfHeight + (Vector2)object1.position;
        o1UR = object1Front * object1HalfWidth + object1Up * object1HalfHeight + (Vector2)object1.position;
        o1DL = -object1Front * object1HalfWidth - object1Up * object1HalfHeight + (Vector2)object1.position;
        o1DR = object1Front * object1HalfWidth - object1Up * object1HalfHeight + (Vector2)object1.position;

        o2UL = -object2Front * object2HalfWidth + object2Up * object2HalfHeight + (Vector2)object2.position;
        o2UR = object2Front * object2HalfWidth + object2Up * object2HalfHeight + (Vector2)object2.position;
        o2DL = -object2Front * object2HalfWidth - object2Up * object2HalfHeight + (Vector2)object2.position;
        o2DR = object2Front * object2HalfWidth - object2Up * object2HalfHeight + (Vector2)object2.position;
    }

    // Funcion que proyecta los vertices de ambos objetos en el eje dado por el vector del objeto y calcula si hay colision en ese eje
    bool ColisionEje(Vector2 objectVectorX)
    {
        float proyectionO1ULinObjectFront = Vector2.Dot(objectVectorX, o1UL);
        float proyectionO1URinObjectFront = Vector2.Dot(objectVectorX, o1UR);
        float proyectionO1DLinObjectFront = Vector2.Dot(objectVectorX, o1DL);
        float proyectionO1DRinObjectFront = Vector2.Dot(objectVectorX, o1DR);
                                       
        float proyectionO2ULinObjectFront = Vector2.Dot(objectVectorX, o2UL);
        float proyectionO2URinObjectFront = Vector2.Dot(objectVectorX, o2UR);
        float proyectionO2DLinObjectFront = Vector2.Dot(objectVectorX, o2DL);
        float proyectionO2DRinObjectFront = Vector2.Dot(objectVectorX, o2DR);

        float maxO1 = CalculateObjectMax(proyectionO1ULinObjectFront, proyectionO1URinObjectFront, proyectionO1DLinObjectFront, proyectionO1DRinObjectFront);
        float minO1 = CalculateObjectMin(proyectionO1ULinObjectFront, proyectionO1URinObjectFront, proyectionO1DLinObjectFront, proyectionO1DRinObjectFront);
        float maxO2 = CalculateObjectMax(proyectionO2ULinObjectFront, proyectionO2URinObjectFront, proyectionO2DLinObjectFront, proyectionO2DRinObjectFront);
        float minO2 = CalculateObjectMin(proyectionO2ULinObjectFront, proyectionO2URinObjectFront, proyectionO2DLinObjectFront, proyectionO2DRinObjectFront);

        return maxO1 >= minO2 && maxO2 >= minO1;
    }

    // Funciones para calcular el maximo y minimo de las proyecciones de los vertices de cada objeto en el eje dado por el vector del objeto
    float CalculateObjectMax(float O1, float O2, float O3, float O4)
    {
        return Mathf.Max(O1, O2, O3, O4);
    }

    float CalculateObjectMin(float O1, float O2, float O3, float O4)
    {
        return Mathf.Min(O1, O2, O3, O4);
    }

}
