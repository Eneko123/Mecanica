using Unity.VisualScripting;
using UnityEngine;

public class ColisionesPuntos : MonoBehaviour
{
    public GameObject colision1;
    public GameObject colision2;

    [SerializeField]private Vector2 XMax1;
    [SerializeField]private Vector2 XMin1;
    [SerializeField]private Vector2 YMax1;
    [SerializeField]private Vector2 YMin1;

    [SerializeField]private Vector2 XMax2;
    [SerializeField]private Vector2 XMin2;
    [SerializeField]private Vector2 YMax2;
    [SerializeField]private Vector2 YMin2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        XMax1 = new Vector2(colision1.transform.position.x + colision1.transform.localScale.x / 2, 0);
        YMax1 = new Vector2(0, colision1.transform.position.y + colision1.transform.localScale.y / 2);
        XMin1 = new Vector2(colision1.transform.position.x - colision1.transform.localScale.x / 2, 0);
        YMin1 = new Vector2(0, colision1.transform.position.y - colision1.transform.localScale.y / 2);

        XMax2 = new Vector2(colision2.transform.position.x + colision2.transform.localScale.x / 2, 0);
        YMax2 = new Vector2(0, colision2.transform.position.y + colision2.transform.localScale.y / 2);
        XMin2 = new Vector2(colision2.transform.position.x - colision2.transform.localScale.x / 2, 0);
        YMin2 = new Vector2(0, colision2.transform.position.y - colision2.transform.localScale.y / 2);

        if (ColisionEjeX() && ColisionEjeY())
        {
            Debug.Log("Colision");
        }
    }

    bool ColisionEjeX()
    {
        if (XMax1.x <= XMax2.x && XMax1.x >= XMin2.x)
        {
            return true;
        }
        else if (XMin1.x <= XMax2.x && XMin1.x >= XMin2.x)
        {
            return true;
        }
        return false;
    }

    bool ColisionEjeY()
    {
        if (YMax1.y <= YMax2.y && YMax1.y >= YMin2.y)
        {
            return true;
        }
        else if (YMin1.y <= YMax2.y && YMin1.y >= YMin2.y)
        {
            return true;
        }
        return false;
    }
}
