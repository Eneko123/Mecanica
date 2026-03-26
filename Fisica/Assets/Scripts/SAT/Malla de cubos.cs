using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Malladecubos : MonoBehaviour
{
    public GameObject cubo;
    public List<GameObject> cuboList;
    int weigth = 40;
    int higth = 40;
    float space = 0.25f;

    float radius = 1f;
    float force = 1f;
    

    private Transform[,] points;

    Vector2 posOrigen;

    void Start()
    {

        DrawWave();
    }
    void Update()
    {
        
    }

    void DrawWave()
    {
        points = new Transform[weigth, higth];
        float offsetX = (weigth - 1) * space * 0.5f * -1;
        float offsetY = (weigth - 1) * space * 0.5f * -1;

        for (int x = 0; x < higth; x++)
        {
            for (int z = 0; z < weigth; z++)
            {
                GameObject c = Instantiate(cubo, transform);
                Vector3 localPoints = new Vector3(
                    offsetX + x * offsetX, 
                    0,
                    offsetY + z * offsetY
                );
                c.transform.localPosition = localPoints;
                points[x,z] = c.transform;
                cuboList.Add(c);
            }
        }
    }

//    if (Input.GetKeyDown(KeyCode.Escape))
//        {
//            points = new Transform[weigth, higth];
//            float sumY = force * space;

//            for (int i = 0; i<cuboList.Count; i++)
//            {
//                cuboList[i].transform.localPosition = new Vector3(cuboList[i].transform.position.x, sumY, cuboList[i].transform.position.z);
//            }
//        }

    float EvaluatePulse(float x, float center, float amplitude, float width, float age)
    {
        float dist = x - center;
        float gaussian = Mathf.Exp(-(dist * dist)/(2f*width*width));
        return amplitude * gaussian;
    }

    void EmitPulse()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            for (int x = 0; x < higth; x++)
            {
                for (int z = 0; z < weigth; z++)
                {
                    // points[x, z];
                }
            }
        }
    }
}
