using UnityEngine;

public class ComportamientoRebano : MonoBehaviour
{
    public float velocidadMaxima = 5f;
    public float radioVecindad = 1.5f;
    public float fuerzaSeparacion = 1f;
    public float fuerzaCohesion = 1f;
    public float fuerzaAlineacion = 1f;

    void Update()
    {
        Vector2 separacion = Separacion();
        Vector2 cohesion = Cohesion();
        Vector2 alineacion = Alineacion();

        // Combina las fuerzas para obtener el movimiento resultante
        Vector2 movimiento = separacion + cohesion + alineacion;
        movimiento = Vector2.ClampMagnitude(movimiento, velocidadMaxima);

        // Aplica el movimiento a la entidad
        transform.Translate(movimiento * Time.deltaTime);
    }

    Vector2 Separacion()
    {
        Vector2 fuerzaSeparacion = Vector2.zero;
        Collider2D[] vecinos = Physics2D.OverlapCircleAll(transform.position, radioVecindad);
        foreach (Collider2D vecino in vecinos)
        {
            if (vecino.gameObject != gameObject)
            {
                Vector2 direccion = transform.position - vecino.transform.position;
                fuerzaSeparacion += direccion.normalized / direccion.magnitude;
            }
        }
        return fuerzaSeparacion.normalized * this.fuerzaSeparacion;
    }

    Vector2 Cohesion()
    {
        Vector2 centroRebano = Vector2.zero;
        int cantidadVecinos = 0;
        Collider2D[] vecinos = Physics2D.OverlapCircleAll(transform.position, radioVecindad);
        foreach (Collider2D vecino in vecinos)
        {
            if (vecino.gameObject != gameObject)
            {
                centroRebano += (Vector2)vecino.transform.position;
                cantidadVecinos++;
            }
        }
        if (cantidadVecinos > 0)
        {
            centroRebano /= cantidadVecinos;
            Vector2 direccion = centroRebano - (Vector2)transform.position;
            return direccion.normalized * this.fuerzaCohesion;
        }
        return Vector2.zero;
    }

    Vector2 Alineacion()
    {
        Vector3 direccionMedia = Vector3.zero;
        int cantidadVecinos = 0;
        Collider2D[] vecinos = Physics2D.OverlapCircleAll(transform.position, radioVecindad);
        foreach (Collider2D vecino in vecinos)
        {
            if (vecino.gameObject != gameObject)
            {
                direccionMedia += ((ComportamientoRebano)vecino.GetComponent(typeof(ComportamientoRebano))).transform.up;
                cantidadVecinos++;
            }
        }
        if (cantidadVecinos > 0)
        {
            direccionMedia /= cantidadVecinos;
            return direccionMedia.normalized * this.fuerzaAlineacion;
        }
        return Vector2.zero;
    }
}

