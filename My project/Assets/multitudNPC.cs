
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class multitudNPC : MonoBehaviour
{
    public float velocidadMaxima = 5f;
    public float radioVecindad = 1.5f;
    public float fuerzaSeparacion = 1f;
    public float fuerzaCohesion = 1f;
    public float fuerzaAlineacion = 1f;
    public Lider liderPos;
    public float VelocidadHaciaElLider;
    public float SeparacionMinimaDelLider;
    private void Awake()
    {
        liderPos = GameObject.Find("Lider").GetComponent<Lider>();  
    }
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
       
        if (Vector3.Distance(transform.position, liderPos.transform.position) >= SeparacionMinimaDelLider)
        {
            transform.position += (liderPos.transform.position - transform.position).normalized * VelocidadHaciaElLider * Time.deltaTime;
        }
      
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
            if (vecino.gameObject != gameObject   &&   vecino.GetComponent<multitudNPC>())
            {
                direccionMedia += ((multitudNPC)vecino.GetComponent(typeof(multitudNPC))).liderPos.transform.position;
                
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

