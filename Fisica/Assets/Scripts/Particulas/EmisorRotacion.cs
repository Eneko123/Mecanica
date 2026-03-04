using UnityEngine;

public class EmisorRotacion : Emisor
{
    [Header("Rotación inicial")]
    public Vector3 velAngularInicial = new Vector3(90f, 90f, 90f);  // grados/s
    public float velAngularVariacion = 45f;

    public Vector3 acelAngularInicial = new Vector3(0f, 0f, 0f);    // grados/s²

    [Header("Física rotación")]
    public float masa = 1f;

    protected override void EmitirParticula()
    {
        // Busca una partícula rotatoria inactiva en el pool
        ParticulasRotatorias p = BuscarInactivaRotatoria();

        if (p == null)
        {
            if (pool.Count < maxParticulas)
            {
                GameObject go = Instantiate(particulaPrefab, PosicionEmision(), Quaternion.identity);
                p = go.GetComponent<ParticulasRotatorias>();
                pool.Add(p);
            }
            else
            {
                return;
            }
        }

        p.gameObject.SetActive(true);
        p.InitRotacion(
            PosicionEmision(),
            VelocidadAleatoria(),
            aceleracion,
            vida,
            masa,
            VelAngularAleatoria(),
            acelAngularInicial
        );
    }

    private ParticulasRotatorias BuscarInactivaRotatoria()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].gameObject.activeInHierarchy && pool[i] is ParticulasRotatorias pr)
                return pr;
        }
        return null;
    }

    private Vector3 VelAngularAleatoria()
    {
        return velAngularInicial + new Vector3(
            Random.Range(-velAngularVariacion, velAngularVariacion),
            Random.Range(-velAngularVariacion, velAngularVariacion),
            Random.Range(-velAngularVariacion, velAngularVariacion)
        );
    }
}