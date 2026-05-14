using UnityEngine;

// Spawner singleton que permite crear nuevos objetos en la escena
// Tecla B: Crear cuadrado (AABB)
// Tecla C: Crear circulo
public class Spawner : MonoBehaviour
{
    // Singleton
    private static Spawner instance;
    public static Spawner Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindFirstObjectByType<Spawner>();

                if (instance == null)
                {
                    GameObject go = new GameObject("Spawner");
                    instance = go.AddComponent<Spawner>();
                }
            }
            return instance;
        }
    }

    [Header("Prefab Settings")]
    [SerializeField] private GameObject squarePrefab;
    [SerializeField] private GameObject circlePrefab;

    private void Awake()
    {
        // Implementar el patron singleton
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Update()
    {
        HandleSpawnInput();
    }

    // Maneja la entrada del teclado para crear objetos
    private void HandleSpawnInput()
    {
        Vector3 spawnPosition = GetMouseWorldPosition();

        // Tecla B: Crear cuadrado
        if (Input.GetKeyDown(KeyCode.B))
        {
            SpawnSquare(spawnPosition);
        }

        // Tecla C: Crear circulo
        if (Input.GetKeyDown(KeyCode.C))
        {
            SpawnCircle(spawnPosition);
        }
    }

    // Crea un cuadrado en la posicion especificada
    public GameObject SpawnSquare(Vector3 position)
    {
        GameObject square;
        square = Instantiate(squarePrefab, position, Quaternion.identity);
        return square;
    }

    // Crea un circulo en la posicion especificada
    public GameObject SpawnCircle(Vector3 position)
    {
        GameObject circle;
        circle = Instantiate(circlePrefab, position, Quaternion.identity);
        return circle;
    }

    // Obtiene la posicion del raton en coordenadas del mundo
    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -Camera.main.transform.position.z; // Distancia de la camara al plano Z=0
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}