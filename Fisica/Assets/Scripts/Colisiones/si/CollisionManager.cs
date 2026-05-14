using System.Collections.Generic;
using UnityEngine;

// Gestor singleton que maneja todas las colisiones entre CustomColliders en la escena
public class CollisionManager : MonoBehaviour
{
    // Singleton instance
    public static CollisionManager instance;

    [Header("Collision Colors")]
    [SerializeField] private Color collisionColor1 = Color.red;

    // Lista de todos los colliders en la escena
    private List<CustomCollider> colliders = new List<CustomCollider>();

    private void Awake()
    {
        // Implementar el patron singleton
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Buscar todos los CustomColliders existentes en la escena
        CustomCollider[] existingColliders = FindObjectsOfType<CustomCollider>();
        foreach (CustomCollider collider in existingColliders)
        {
            if (!colliders.Contains(collider))
            {
                colliders.Add(collider);
            }
        }
    }

    private void Update()
    {
        CheckAllCollisions();
    }

    // Registra un nuevo collider en el gestor
    public void RegisterCollider(CustomCollider collider)
    {
        if (!colliders.Contains(collider))
        {
            colliders.Add(collider);
        }
    }

    // Desregistra un collider del gestor
    public void UnregisterCollider(CustomCollider collider)
    {
        if (colliders.Contains(collider))
        {
            colliders.Remove(collider);
        }
    }

    // Comprueba todas las posibles colisiones entre colliders
    private void CheckAllCollisions()
    {
        // Primero resetear el estado de colision de todos los objetos
        foreach (CustomCollider collider in colliders)
        {
            if (collider != null)
            {
                collider.SetCollisionState(false);
            }
        }

        // Comprobar colisiones entre cada par de objetos
        for (int i = 0; i < colliders.Count; i++)
        {
            if (colliders[i] == null) continue;

            for (int j = i + 1; j < colliders.Count; j++)
            {
                if (colliders[j] == null) continue;

                // Comprobar colision entre el objeto i y el objeto j
                bool collision = CheckCollisionBetween(colliders[i], colliders[j]);

                if (collision)
                {
                    // Marcar ambos objetos como colisionando
                    colliders[i].SetCollisionState(true);
                    colliders[j].SetCollisionState(true);
                }
            }
        }
    }

    // Comprueba la colision entre dos colliders especificos
    private bool CheckCollisionBetween(CustomCollider collider1, CustomCollider collider2)
    {
        // El CustomCollider maneja todas las combinaciones
        return collider1.CheckCollisionWith(collider2);
    }

    /// Obtiene el número de colliders registrados
    //public int GetColliderCount()
    //{
    //    return colliders.Count;
    //}

    /// <summary>
    /// Limpia la lista de colliders nulos
    /// </summary>
    //public void CleanUpNullColliders()
    //{
    //    colliders.RemoveAll(c => c == null);
    //}

    
}