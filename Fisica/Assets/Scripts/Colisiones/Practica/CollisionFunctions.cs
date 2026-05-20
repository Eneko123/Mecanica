using UnityEngine;

// Clase estatica que contiene todas las funciones de deteccion de colisiones
// PointToAABB, PointToCircle, PointToOBB
// AABBToAABB, CircleToAABB, CircleToCircle, CircleToOBB
public static class CollisionFunctions
{
    // Colisiones Punto-Volumen 


    // Detecta colision entre un punto y un AABB
    public static bool PointToAABB(Vector2 point, Vector2 aabbCenter, Vector2 aabbSize)
    {
        float halfWidth = aabbSize.x * 0.5f;
        float halfHeight = aabbSize.y * 0.5f;

        float minX = aabbCenter.x - halfWidth;
        float maxX = aabbCenter.x + halfWidth;
        float minY = aabbCenter.y - halfHeight;
        float maxY = aabbCenter.y + halfHeight;

        return point.x >= minX && point.x <= maxX &&
               point.y >= minY && point.y <= maxY;
    }

    // Detecta colision entre un punto y un circulo
    public static bool PointToCircle(Vector2 point, Vector2 circleCenter, float radius)
    {
        float distance = Vector2.Distance(point, circleCenter);
        return distance <= radius;
    }

    // Detecta colision entre un punto y un OBB
    public static bool PointToOBB(Vector2 point, Vector2 obbCenter, Vector2 obbSize, float obbRotation)
    {
        // Convertir la rotacion a radianes
        float angleRad = obbRotation * Mathf.Deg2Rad;

        // Vectores de direccion del OBB
        Vector2 right = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
        Vector2 up = new Vector2(-Mathf.Sin(angleRad), Mathf.Cos(angleRad));

        // Vector del centro al punto
        Vector2 centerToPoint = point - obbCenter;

        // Proyectar el vector en los ejes locales del OBB
        float projectionX = Vector2.Dot(centerToPoint, right);
        float projectionY = Vector2.Dot(centerToPoint, up);

        // Comparar con las dimensiones
        float halfWidth = obbSize.x * 0.5f;
        float halfHeight = obbSize.y * 0.5f;

        return Mathf.Abs(projectionX) <= halfWidth &&
               Mathf.Abs(projectionY) <= halfHeight;
    }

    // Colisiones Volumen-Volumen 

    // Detecta colision entre dos AABBs
    public static bool AABBToAABB(Vector2 center1, Vector2 size1, Vector2 center2, Vector2 size2)
    {
        float halfWidth1 = size1.x * 0.5f;
        float halfHeight1 = size1.y * 0.5f;
        float halfWidth2 = size2.x * 0.5f;
        float halfHeight2 = size2.y * 0.5f;

        float minX1 = center1.x - halfWidth1;
        float maxX1 = center1.x + halfWidth1;
        float minY1 = center1.y - halfHeight1;
        float maxY1 = center1.y + halfHeight1;

        float minX2 = center2.x - halfWidth2;
        float maxX2 = center2.x + halfWidth2;
        float minY2 = center2.y - halfHeight2;
        float maxY2 = center2.y + halfHeight2;

        // Hay colision si hay solapamiento en ambos ejes
        bool overlapX = maxX1 >= minX2 && maxX2 >= minX1;
        bool overlapY = maxY1 >= minY2 && maxY2 >= minY1;

        return overlapX && overlapY;
    }

    // Detecta colision entre un circulo y un AABB
    public static bool CircleToAABB(Vector2 circleCenter, float radius, Vector2 aabbCenter, Vector2 aabbSize)
    {
        float halfWidth = aabbSize.x * 0.5f;
        float halfHeight = aabbSize.y * 0.5f;

        // Encontrar el punto mas cercano del AABB al centro del circulo
        float closestX = Mathf.Clamp(circleCenter.x, aabbCenter.x - halfWidth, aabbCenter.x + halfWidth);
        float closestY = Mathf.Clamp(circleCenter.y, aabbCenter.y - halfHeight, aabbCenter.y + halfHeight);

        Vector2 closestPoint = new Vector2(closestX, closestY);

        // Calcular la distancia entre el punto mas cercano y el centro del circulo
        float distance = Vector2.Distance(closestPoint, circleCenter);

        return distance <= radius;
    }

    // Detecta colision entre dos circulos
    public static bool CircleToCircle(Vector2 center1, float radius1, Vector2 center2, float radius2)
    {
        float distance = Vector2.Distance(center1, center2);
        float sumRadii = radius1 + radius2;

        return distance <= sumRadii;
    }

    // Detecta colision entre un circulo y un OBB
    public static bool CircleToOBB(Vector2 circleCenter, float radius, Vector2 obbCenter, Vector2 obbSize, float obbRotation)
    {
        // Convertir la rotacion a radianes
        float angleRad = obbRotation * Mathf.Deg2Rad;

        // Vectores de direccion del OBB
        Vector2 right = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
        Vector2 up = new Vector2(-Mathf.Sin(angleRad), Mathf.Cos(angleRad));

        // Vector del OBB al circulo
        Vector2 obbToCircle = circleCenter - obbCenter;

        // Proyectar el vector en los ejes locales del OBB
        float projectionX = Vector2.Dot(obbToCircle, right);
        float projectionY = Vector2.Dot(obbToCircle, up);

        // Clampear a las dimensiones del OBB
        float halfWidth = obbSize.x * 0.5f;
        float halfHeight = obbSize.y * 0.5f;

        float clampedX = Mathf.Clamp(projectionX, -halfWidth, halfWidth);
        float clampedY = Mathf.Clamp(projectionY, -halfHeight, halfHeight);

        // Punto mas cercano en el OBB en coordenadas locales
        Vector2 closestLocalPoint = new Vector2(clampedX, clampedY);

        // Convertir de vuelta a coordenadas globales
        Vector2 closestPoint = obbCenter + right * closestLocalPoint.x + up * closestLocalPoint.y;

        // Calcular la distancia entre el punto mas cercano y el centro del circulo
        float distance = Vector2.Distance(closestPoint, circleCenter);

        return distance <= radius;
    }

    // Detecta colision entre dos OBBs 
    public static bool OBBToOBB(Vector2 center1, Vector2 size1, float rotation1,
                                 Vector2 center2, Vector2 size2, float rotation2)
    {
        // Convertir rotaciones a radianes
        float angle1 = rotation1 * Mathf.Deg2Rad;
        float angle2 = rotation2 * Mathf.Deg2Rad;

        // Ejes del primer OBB
        Vector2 axis1Right = new Vector2(Mathf.Cos(angle1), Mathf.Sin(angle1));
        Vector2 axis1Up = new Vector2(-Mathf.Sin(angle1), Mathf.Cos(angle1));

        // Ejes del segundo OBB
        Vector2 axis2Right = new Vector2(Mathf.Cos(angle2), Mathf.Sin(angle2));
        Vector2 axis2Up = new Vector2(-Mathf.Sin(angle2), Mathf.Cos(angle2));

        // Verificar los 4 ejes posibles (2 por cada OBB)
        Vector2[] axes = { axis1Right, axis1Up, axis2Right, axis2Up };

        foreach (Vector2 axis in axes)
        {
            if (!ProjectionOverlap(axis, center1, size1, angle1, center2, size2, angle2))
            {
                return false; // Encontramos un eje separador
            }
        }

        return true; // No hay eje separador, hay colision
    }

    // Metodo auxiliar para verificar si las proyecciones de dos OBBs se solapan en un eje
    private static bool ProjectionOverlap(Vector2 axis, Vector2 center1, Vector2 size1, float angle1,
                                          Vector2 center2, Vector2 size2, float angle2)
    {
        // Calcular vertices del primer OBB
        Vector2[] vertices1 = GetOBBVertices(center1, size1, angle1);
        Vector2[] vertices2 = GetOBBVertices(center2, size2, angle2);

        // Proyectar todos los vertices en el eje
        float min1 = float.MaxValue, max1 = float.MinValue;
        foreach (Vector2 vertex in vertices1)
        {
            float projection = Vector2.Dot(vertex, axis);
            min1 = Mathf.Min(min1, projection);
            max1 = Mathf.Max(max1, projection);
        }

        float min2 = float.MaxValue, max2 = float.MinValue;
        foreach (Vector2 vertex in vertices2)
        {
            float projection = Vector2.Dot(vertex, axis);
            min2 = Mathf.Min(min2, projection);
            max2 = Mathf.Max(max2, projection);
        }

        // Verificar solapamiento
        return max1 >= min2 && max2 >= min1;
    }

    // Metodo auxiliar para obtener los 4 vertices de un OBB
    private static Vector2[] GetOBBVertices(Vector2 center, Vector2 size, float angleRad)
    {
        Vector2 right = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
        Vector2 up = new Vector2(-Mathf.Sin(angleRad), Mathf.Cos(angleRad));

        float halfWidth = size.x * 0.5f;
        float halfHeight = size.y * 0.5f;

        Vector2[] vertices = new Vector2[4];
        vertices[0] = center - right * halfWidth + up * halfHeight; // Top-left
        vertices[1] = center + right * halfWidth + up * halfHeight; // Top-right
        vertices[2] = center - right * halfWidth - up * halfHeight; // Bottom-left
        vertices[3] = center + right * halfWidth - up * halfHeight; // Bottom-right

        return vertices;
    }
}