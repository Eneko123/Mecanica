using UnityEngine;

// Componente que define un collider personalizado para detectar colisiones
// AABB (cuadrado sin rotar), Circle (circulo), y OBB (cuadrado rotado)
public class CustomCollider : MonoBehaviour
{
    public enum ColliderType
    {
        AABB,    // Cuadrado sin rotar (Axis-Aligned Bounding Box)
        Circle,  // Circulo
        OBB      // Cuadrado rotado (Oriented Bounding Box)
    }

    [Header("Collider Settings")]
    [SerializeField] private ColliderType colliderType = ColliderType.AABB;

    [Header("Visual Settings")]
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color collisionColor = Color.red;
    [SerializeField] private Color selectedColor = Color.yellow;

    // Referencia al renderer del objeto
    private SpriteRenderer spriteRenderer;
    private Renderer meshRenderer;

    // Estado del collider
    private bool isSelected = false;
    private bool isColliding = false;
    private Vector3 offset;

    // Constantes para rotacion
    private const float ROTATION_SPEED = 100f;

    // Propiedades publicas
    public ColliderType Type => colliderType;
    public Vector2 Center => transform.position;
    public Vector2 Size => transform.localScale;
    public float Rotation => transform.eulerAngles.z;

    private void Awake()
    {
        // Intentar obtener el renderer del objeto
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            meshRenderer = GetComponent<Renderer>();
        }

        // Registrar este collider en el CollisionManager
        if (CollisionManager.instance != null)
        {
            CollisionManager.instance.RegisterCollider(this);
        }
    }

    private void OnDestroy()
    {
        // Desregistrar cuando se destruye
        if (CollisionManager.instance != null)
        {
            CollisionManager.instance.UnregisterCollider(this);
        }
    }

    private void Update()
    {
        HandleMouseInput();
        HandleRotation();
        UpdateVisual();
    }

    // Maneja la entrada del raton para seleccionar y mover el objeto
    private void HandleMouseInput()
    {
        Vector3 mousePosition = GetMouseWorldPosition();

        // Detectar si se pulsa el raton
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos2D = new Vector2(mousePosition.x, mousePosition.y);

            // Comprobar si el raton esta sobre este objeto
            if (IsPointInside(mousePos2D))
            {
                isSelected = true;
                offset = transform.position - mousePosition;
            }
        }

        // Si esta seleccionado, mover con el raton
        if (isSelected && Input.GetMouseButton(0))
        {
            transform.position = mousePosition + offset;
        }

        // Deseleccionar al soltar el boton
        if (Input.GetMouseButtonUp(0))
        {
            isSelected = false;
        }
    }

    // Maneja la rotacion con las teclas Q y E
    private void HandleRotation()
    {
        if (isSelected)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                transform.Rotate(0f, 0f, ROTATION_SPEED * Time.deltaTime);
                UpdateColliderType();
            }
            else if (Input.GetKey(KeyCode.E))
            {
                transform.Rotate(0f, 0f, -ROTATION_SPEED * Time.deltaTime);
                UpdateColliderType();
            }
        }
    }

    // Actualiza el tipo de collider segun la rotacion (AABB vs OBB)
    private void UpdateColliderType()
    {
        if (colliderType == ColliderType.Circle)
            return; // Los circulos no cambian de tipo

        // Si la rotacion es aproximadamente 0, es AABB, de lo contrario es OBB
        float angle = Mathf.Abs(transform.eulerAngles.z % 360f);

        // Considerar AABB si esta cerca de 0, 90, 180 o 270 grados (con un margen de error)
        const float threshold = 1f;
        bool isAxisAligned = (angle < threshold) ||
                            (Mathf.Abs(angle - 90f) < threshold) ||
                            (Mathf.Abs(angle - 180f) < threshold) ||
                            (Mathf.Abs(angle - 270f) < threshold) ||
                            (angle > 360f - threshold);

        colliderType = isAxisAligned ? ColliderType.AABB : ColliderType.OBB;
    }

    // Verifica si un punto esta dentro de este collider
    public bool IsPointInside(Vector2 point)
    {
        switch (colliderType)
        {
            case ColliderType.AABB:
                return CollisionFunctions.PointToAABB(point, Center, Size);

            case ColliderType.Circle:
                float radius = Size.x * 0.5f; // Asumimos que el circulo es uniforme
                return CollisionFunctions.PointToCircle(point, Center, radius);

            case ColliderType.OBB:
                return CollisionFunctions.PointToOBB(point, Center, Size, Rotation);

            default:
                return false;
        }
    }

    // Verifica si este collider colisiona con otro
    public bool CheckCollisionWith(CustomCollider other)
    {
        if (other == this)
            return false;

        // AABB con AABB
        if (this.Type == ColliderType.AABB && other.Type == ColliderType.AABB)
        {
            return CollisionFunctions.AABBToAABB(this.Center, this.Size, other.Center, other.Size);
        }
        // Circle con Circle
        else if (this.Type == ColliderType.Circle && other.Type == ColliderType.Circle)
        {
            float radius1 = this.Size.x * 0.5f;
            float radius2 = other.Size.x * 0.5f;
            return CollisionFunctions.CircleToCircle(this.Center, radius1, other.Center, radius2);
        }
        // Circle con AABB
        else if (this.Type == ColliderType.Circle && other.Type == ColliderType.AABB)
        {
            float radius = this.Size.x * 0.5f;
            return CollisionFunctions.CircleToAABB(this.Center, radius, other.Center, other.Size);
        }
        // AABB con Circle
        else if (this.Type == ColliderType.AABB && other.Type == ColliderType.Circle)
        {
            float radius = other.Size.x * 0.5f;
            return CollisionFunctions.CircleToAABB(other.Center, radius, this.Center, this.Size);
        }
        // Circle con OBB
        else if (this.Type == ColliderType.Circle && other.Type == ColliderType.OBB)
        {
            float radius = this.Size.x * 0.5f;
            return CollisionFunctions.CircleToOBB(this.Center, radius, other.Center, other.Size, other.Rotation);
        }
        // OBB con Circle
        else if (this.Type == ColliderType.OBB && other.Type == ColliderType.Circle)
        {
            float radius = other.Size.x * 0.5f;
            return CollisionFunctions.CircleToOBB(other.Center, radius, this.Center, this.Size, this.Rotation);
        }
        // OBB con OBB
        else if (this.Type == ColliderType.OBB && other.Type == ColliderType.OBB)
        {
            return CollisionFunctions.OBBToOBB(this.Center, this.Size, this.Rotation,
                                                other.Center, other.Size, other.Rotation);
        }
        // OBB con AABB (tratamos el AABB como OBB con rotacion 0)
        else if (this.Type == ColliderType.OBB && other.Type == ColliderType.AABB)
        {
            return CollisionFunctions.OBBToOBB(this.Center, this.Size, this.Rotation,
                                                other.Center, other.Size, 0f);
        }
        // AABB con OBB
        else if (this.Type == ColliderType.AABB && other.Type == ColliderType.OBB)
        {
            return CollisionFunctions.OBBToOBB(this.Center, this.Size, 0f,
                                                other.Center, other.Size, other.Rotation);
        }

        return false;
    }

    // Establece el estado de colision para actualizar el color
    public void SetCollisionState(bool colliding)
    {
        isColliding = colliding;
    }

    // Actualiza el color del objeto segun su estado
    private void UpdateVisual()
    {
        Color targetColor = normalColor;

        if (isSelected)
        {
            targetColor = selectedColor;
        }
        else if (isColliding)
        {
            targetColor = collisionColor;
        }

        // Aplicar el color al renderer correspondiente
        if (spriteRenderer != null)
        {
            spriteRenderer.color = targetColor;
        }
        else if (meshRenderer != null)
        {
            meshRenderer.material.color = targetColor;
        }
    }

    // Obtiene la posicion del raton en coordenadas del mundo
    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -Camera.main.transform.position.z; // Distancia de la camara al plano Z=0
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    // Metodo para forzar el tipo de collider (usado por el Spawner)
    public void SetColliderType(ColliderType type)
    {
        colliderType = type;
    }
}