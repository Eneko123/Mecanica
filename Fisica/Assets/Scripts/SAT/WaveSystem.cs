using System.Collections.Generic;
using UnityEngine;

// Sistema principal de ondas que gestiona la grid de puntos y la propagacion de ondas
public class WaveSystem : MonoBehaviour
{
    [Header("Grid Configuration")]
    [SerializeField] private int gridSizeX = 20;
    [SerializeField] private int gridSizeZ = 20;
    [SerializeField] private float gridSpacing = 0.5f;
    [SerializeField] private GameObject pointPrefab; // Prefab de cubo para cada punto

    [Header("Wave Configuration")]
    [SerializeField] private KeyCode emitKey = KeyCode.Space;
    [SerializeField] private float waveSpeed = 3f;
    [SerializeField] private float waveAmplitude = 1f;
    [SerializeField] private float pulseWidth = 2f;

    [Header("Wave Decay (Extra)")]
    [SerializeField] private bool useDecay = true;
    [SerializeField] private float maxWaveAge = 5f; // Edad maxima antes de eliminar
    [SerializeField] private float decayRate = 0.3f; // Velocidad de decrecimiento

    [Header("Emit Position")]
    [SerializeField] private Transform emitPoint; // Punto desde donde se emiten las ondas

    // Estructura para almacenar informacion de cada onda
    private struct Wave
    {
        public float startTime;
        public Vector2 position;
        public float speed;
        public float amplitude;
        public float width;
    }

    private WavePoint[,] grid;
    private List<Wave> activeWaves = new List<Wave>();

    void Start()
    {
        GenerateGrid();
    }

    void Update()
    {
        // Generar nueva onda al pulsar la tecla
        if (Input.GetKeyDown(emitKey))
        {
            EmitWave();
        }

        // Actualizar todas las ondas
        UpdateWaves();

        // Eliminar ondas viejas (Extra)
        if (useDecay)
        {
            RemoveOldWaves();
        }
    }

    // Genera la grid de puntos X por Z
    void GenerateGrid()
    {
        grid = new WavePoint[gridSizeX, gridSizeZ];

        // Calcular offset para centrar la grid
        float offsetX = (gridSizeX - 1) * gridSpacing * 0.5f;
        float offsetZ = (gridSizeZ - 1) * gridSpacing * 0.5f;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int z = 0; z < gridSizeZ; z++)
            {
                // Calcular posicion del punto
                Vector3 position = new Vector3(
                    x * gridSpacing - offsetX,
                    0,
                    z * gridSpacing - offsetZ
                );

                // Crear el punto
                GameObject pointObj = Instantiate(pointPrefab, position, Quaternion.identity, transform);
                pointObj.name = $"WavePoint_{x}_{z}";

                // Configurar el componente WavePoint
                WavePoint point = pointObj.GetComponent<WavePoint>();
                if (point == null)
                {
                    point = pointObj.AddComponent<WavePoint>();
                }

                point.GridPosition = new Vector2(position.x, position.z);
                grid[x, z] = point;
            }
        }
    }

    // Emite una nueva onda desde la posicion configurada
    void EmitWave()
    {
        Vector3 emitPosition = emitPoint != null ? emitPoint.position : transform.position;

        Wave newWave = new Wave
        {
            startTime = Time.time,
            position = new Vector2(emitPosition.x, emitPosition.z),
            speed = waveSpeed,
            amplitude = waveAmplitude,
            width = pulseWidth
        };

        activeWaves.Add(newWave);
    }

    // Actualiza todas las ondas activas y calcula la altura de cada punto
    void UpdateWaves()
    {
        float currentTime = Time.time;

        // Para cada punto de la grid
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int z = 0; z < gridSizeZ; z++)
            {
                WavePoint point = grid[x, z];
                float totalHeight = 0f;

                // Calcular la influencia de todas las ondas (superposicion)
                foreach (Wave wave in activeWaves)
                {
                    float age = currentTime - wave.startTime;
                    if (age < 0) continue;

                    // Calcular el radio actual de la onda
                    float radius = wave.speed * age;

                    // Distancia del punto al centro de la onda
                    float distance = Vector2.Distance(point.GridPosition, wave.position);

                    // Evaluar la influencia usando un pulso gaussiano
                    float influence = EvaluateGaussianPulse(distance, radius, wave.amplitude, wave.width, age);

                    totalHeight += influence;
                }

                // Actualizar la altura del punto
                point.UpdateHeight(totalHeight);
            }
        }
    }

    // Evalua la influencia de una onda en un punto usando un pulso gaussiano
    float EvaluateGaussianPulse(float distance, float radius, float amplitude, float width, float age)
    {
        // Diferencia entre la distancia del punto y el radio actual de la onda
        float diff = distance - radius;

        // Funcion gaussiana centrada en el radio de la onda
        float gaussian = Mathf.Exp(-(diff * diff) / (2f * width * width));

        // Aplicar decrecimiento de amplitud con el tiempo (Extra)
        float currentAmplitude = amplitude;
        if (useDecay)
        {
            float decayFactor = 1f - (age / maxWaveAge);
            decayFactor = Mathf.Clamp01(decayFactor);
            currentAmplitude *= decayFactor;
        }

        return currentAmplitude * gaussian;
    }

    // Elimina ondas que han superado la edad maxima (Extra)
    void RemoveOldWaves()
    {
        float currentTime = Time.time;
        activeWaves.RemoveAll(wave => (currentTime - wave.startTime) > maxWaveAge);
    }

    // Obtiene la altura del agua en una posicion XZ especifica
    // Usado por el sistema de flotacion
    public float GetWaterHeightAt(Vector2 position)
    {
        float currentTime = Time.time;
        float totalHeight = 0f;

        // Calcular la influencia de todas las ondas en esta posicion
        foreach (Wave wave in activeWaves)
        {
            float age = currentTime - wave.startTime;
            if (age < 0) continue;

            float radius = wave.speed * age;
            float distance = Vector2.Distance(position, wave.position);

            float influence = EvaluateGaussianPulse(distance, radius, wave.amplitude, wave.width, age);
            totalHeight += influence;
        }

        return totalHeight;
    }


    // Dibuja gizmos para visualizar la grid y las ondas activas
    void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        // Dibujar ondas activas
        Gizmos.color = Color.cyan;
        float currentTime = Time.time;

        foreach (Wave wave in activeWaves)
        {
            float age = currentTime - wave.startTime;
            float radius = wave.speed * age;

            Vector3 center = new Vector3(wave.position.x, 0, wave.position.y);
            DrawCircle(center, radius, 32);
        }

        // Dibujar punto de emision
        if (emitPoint != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(emitPoint.position, 0.3f);
        }
    }

    void DrawCircle(Vector3 center, float radius, int segments)
    {
        float angleStep = 360f / segments;
        Vector3 prevPoint = center + new Vector3(radius, 0, 0);

        for (int i = 1; i <= segments; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            Vector3 newPoint = center + new Vector3(
                Mathf.Cos(angle) * radius,
                0,
                Mathf.Sin(angle) * radius
            );

            Gizmos.DrawLine(prevPoint, newPoint);
            prevPoint = newPoint;
        }
    }
}
