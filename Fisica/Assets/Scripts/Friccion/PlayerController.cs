using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Space(5)]
    // Sirve para ver la direccion en la que se mueve
    public Vector2 MoveDir = Vector2.zero;

    // Hace referencia al componente CharacterController del objeto
    private CharacterController controller;

    // Velocidad del jugador para moverse
    [SerializeField] float speed = 4f;

    // Sirve para obtener la direccion de la camara y alinear el movimiento / Raycast
    [SerializeField] CameraPlayer cameraPlayer;

    // Capa para que el Raycast solo detecte cajas
    [SerializeField] LayerMask cajaLayer;

    // Controla el si se puede mover el jugador o no
    private bool _movementInputPressed = false;

    // Guarda la posicion y rotacion inicial para reiniciar
    private Vector3 startPos;
    private Quaternion startRot;

    // Variables para empujar cajas
    private GameObject cajaDetectada;
    private float chargeTimer = 0f;
    private bool estaCargando = false;
    [SerializeField] float pushForce = 12f;
    [SerializeField] float maxChargeTime = 2f;

    // Singleton para que otros scripts accedan al jugador
    public static PlayerController Instance { get; private set; }
    public Transform playerTransform;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            playerTransform = transform;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        controller = GetComponent<CharacterController>();
        // Guardamos la posicion inicial al arrancar
        startPos = transform.position;
        startRot = transform.rotation;
    }

    private void Update()
    {
        // Reiniciar con tecla R (funciona aunque no lo mapees en InputActions)
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            ResetPlayer();
        }

        // Deteccion continua de caja delante del jugador
        DetectarCajaDelante();

        // Carga de empuje mientras se mantiene clic izquierdo
        if (Mouse.current.leftButton.isPressed)
        {
            estaCargando = true;
            chargeTimer += Time.deltaTime;
        }
        else if (estaCargando)
        {
            EmpujarCaja();
            estaCargando = false;
            chargeTimer = 0f;
        }

        // Movimiento bAsico
        if (_movementInputPressed)
        {
            Vector3 move = (transform.forward * MoveDir.y + cameraPlayer.transform.right * MoveDir.x);
            controller.Move(move.normalized * speed * Time.deltaTime);
        }
    }

    // Se llama al evento en Unity asociado con la accion de moverse
    public void OnMove(InputAction.CallbackContext contextMove)
    {
        if (contextMove.started)
        {
            _movementInputPressed = true;
        }
        else if (contextMove.canceled)
        {
            _movementInputPressed = false;
        }
        MoveDir = contextMove.ReadValue<Vector2>();
    }

    // Se llama al evento en Unity asociado con la accion de reiniciar posicion
    public void OnResetPosition(InputAction.CallbackContext contextReset)
    {
        if (contextReset.performed)
        {
            ResetPlayer();
        }
    }

    // Detecta la caja mas cercana en la direccion de la camara
    private void DetectarCajaDelante()
    {
        RaycastHit hit;
        Vector3 origen = transform.position + Vector3.up * 0.8f;
        // Raycast de 2.5 metros hacia adelante
        if (Physics.Raycast(origen, cameraPlayer.transform.forward, out hit, 2.5f, cajaLayer))
        {
            if (hit.collider.CompareTag("Caja"))
                cajaDetectada = hit.collider.gameObject;
            else
                cajaDetectada = null;
        }
        else
        {
            cajaDetectada = null;
        }
    }

    // Aplica impulso a la caja detectada
    private void EmpujarCaja()
    {
        if (cajaDetectada == null) return;

        Rigidbody rbCaja = cajaDetectada.GetComponent<Rigidbody>();
        if (rbCaja != null)
        {
            // Calcula la fuerza segun el tiempo que se mantuvo pulsado
            float fuerza = pushForce * Mathf.Clamp(chargeTimer / maxChargeTime, 0.2f, 1f);
            Vector3 direccion = (cajaDetectada.transform.position - transform.position).normalized;
            rbCaja.AddForce(direccion * fuerza, ForceMode.Impulse);
        }
    }

    // Reinicia posicion, rotacion y estados de movimiento
    public void ResetPlayer()
    {
        // CharacterController no usa velocidades fisicas, pero desactivarlo/activarlo evita atascos
        controller.enabled = false;
        transform.position = startPos;
        transform.rotation = startRot;
        controller.enabled = true;

        // Limpiamos estados para evitar empujes fantasma o inputs bloqueados
        cajaDetectada = null;
        chargeTimer = 0f;
        estaCargando = false;
        MoveDir = Vector2.zero;
        _movementInputPressed = false;
    }

    private void OnDrawGizmos()
    {
        if (cameraPlayer != null)
        {
            Vector3 origen = transform.position + Vector3.up * 0.5f;
            Gizmos.color = cajaDetectada != null ? Color.green : Color.red;
            Gizmos.DrawRay(origen, cameraPlayer.transform.forward * 2.5f);
        }
    }
}