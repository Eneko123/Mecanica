using System.Collections.Generic;
using UnityEngine;

public class Emisor : MonoBehaviour
{
    public GameObject particulaPrefab;
    public float spawnRate = 0.1f;
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnRate)
        {
            GameObject p = Instantiate(particulaPrefab, transform.position, Quaternion.identity);
            p.SetActive(true);
            timer = 0;
        }
    }
}
