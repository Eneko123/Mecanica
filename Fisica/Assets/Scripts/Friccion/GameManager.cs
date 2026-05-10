using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("UI")]
    [SerializeField] TextMeshProUGUI textoUI;
    [SerializeField] GameObject panelVictoria;
    [SerializeField] GameObject panelDerrota;
    [SerializeField] Button reiniciarButton;

    [Header("Configuracion")]
    [SerializeField] float tiempoLimite = 60f;

    private int totalCajas;
    private int entregadas;
    private float tiempoInicio;
    private bool juegoTerminado;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        totalCajas = FindObjectsByType<Caja>(FindObjectsSortMode.None).Length;
        tiempoInicio = Time.time;
        entregadas = 0;
        juegoTerminado = false;

        if (panelVictoria) panelVictoria.SetActive(false);
        if (panelDerrota) panelDerrota.SetActive(false);

        reiniciarButton.onClick.AddListener(() => ReiniciarNivel());
    }

    void Update()
    {
        if (!juegoTerminado)
        {
            float tiempoTranscurrido = Time.time - tiempoInicio;
            float tiempoRestante = tiempoLimite - tiempoTranscurrido;

            // Actualizar UI con tiempo restante
            if (textoUI)
            {
                textoUI.text = $"Entregas: {entregadas}/{totalCajas} | Tiempo: {tiempoRestante:F1}s";
            }

            // Comprobar si se acabo el tiempo
            if (tiempoRestante <= 0)
            {
                Derrota();
            }
        }
    }

    public void RegistrarEntrega()
    {
        if (juegoTerminado) return;

        entregadas++;

        if (entregadas >= totalCajas)
        {
            Victoria();
        }
    }

    void Victoria()
    {
        juegoTerminado = true;
        float tiempoFinal = Time.time - tiempoInicio;

        if (textoUI)
        {
            textoUI.text = $"¡VICTORIA! Tiempo final: {tiempoFinal:F1}s";
        }

        if (panelVictoria)
        {
            panelVictoria.SetActive(true);
        }
    }

    void Derrota()
    {
        juegoTerminado = true;

        Cursor.lockState = CursorLockMode.None;

        if (textoUI)
        {
            textoUI.text = $"¡SE ACABO EL TIEMPO! Entregas: {entregadas}/{totalCajas}";
        }

        if (panelDerrota)
        {
            panelDerrota.SetActive(true);
        }
    }

    // Reiniciar nivel completo (desde UI o tecla)
    public void ReiniciarNivel()
    {
        // Reiniciar todas las cajas
        ReiniciarCajas();
        

        // Reiniciar jugador
        if (PlayerController.Instance != null)
        {
            PlayerController.Instance.ResetPlayer();
        }

        // Reiniciar contadores
        entregadas = 0;
        juegoTerminado = false;
        tiempoInicio = Time.time; // REINICIA EL TIEMPO

        // Ocultar paneles
        if (panelVictoria) panelVictoria.SetActive(false);
        if (panelDerrota) panelDerrota.SetActive(false);
    }

    // Reiniciar solo cajas 
    public void ReiniciarCajas()
    {
        // Reiniciar todas las cajas
        foreach (Caja c in FindObjectsByType<Caja>(FindObjectsSortMode.None))
        {
            c.ResetCaja();
        }
        // Reiniciar contador de entregas
        entregadas = 0;
    }
}
