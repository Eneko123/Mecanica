using System.Collections.Generic;
using UnityEngine;

public class SFmanager : MonoBehaviour
{
    [SerializeField] public List<Force> balls = new List<Force>();
    public float xmin=-10, xmax=10, ymin=-10, ymax=10;

    void Update() 
    { 
        foreach (var b in balls) 
        {
            b.Resolve(Time.deltaTime);
            ResolveWallColision(b);
        }
    }

    void ResolveWallColision(Force e)
    { 
        Vector3 p3= e.transform.position;
        Vector2 pos = new Vector2(p3.x, p3.y);

        if (pos.x - e.rad < xmin)
        {
            pos.x = xmin + e.rad;
            e.vel.x = -e.vel.x;
        }
        else if (pos.x + e.rad > xmax)
        {
            pos.x = xmax - e.rad;
            e.vel.x = -e.vel.x;
        }

        if (pos.y + e.rad < ymin)
        {
            pos.y = ymin - e.rad;
            e.vel.y = -e.vel.y;
        }
        else if (pos.y - e.rad > ymax)
        {
            pos.y = ymax + e.rad;
            e.vel.y = -e.vel.y;
        }
        e.transform.position = pos;
    }
}
