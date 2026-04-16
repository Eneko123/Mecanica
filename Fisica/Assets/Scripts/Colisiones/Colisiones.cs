using UnityEngine;

public class Colisiones : MonoBehaviour
{
    public GameObject colision1;
    public GameObject colision2;
    private float tamanioX1;
    private float tamanioY1;
    private float tamanioX2;
    private float tamanioY2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tamanioX1 = colision1.transform.localScale.x;
        tamanioY1 = colision1.transform.localScale.y;
        tamanioX2 = colision2.transform.localScale.x;
        tamanioY2 = colision2.transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceX = Mathf.Abs(colision1.transform.position.x - colision2.transform.position.x);
        float distanceY = Mathf.Abs(colision1.transform.position.y - colision2.transform.position.y);

        float totalTamanioX = (tamanioX1 + tamanioX2) / 2;
        float totalTamanioY = (tamanioY1 + tamanioY2) / 2;

        if (distanceX < totalTamanioX && distanceY < totalTamanioY)
        {
            Debug.Log("Colision");
        }
    }
}
