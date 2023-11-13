using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particulasdir : MonoBehaviour
{

    public float rango = 50f; // Rango de detecci�n
    public ParticleSystem sistemaDeParticulas; // Referencia al sistema de part�culas

    // Start is called before the first frame update
    void Start()
    {
        if (sistemaDeParticulas == null)
        {
            // Si no se asigna un sistema de part�culas, intenta obtenerlo autom�ticamente
            sistemaDeParticulas = GetComponent<ParticleSystem>();
        }

        if (sistemaDeParticulas == null)
        {
            Debug.LogError("Se requiere un componente ParticleSystem en el objeto o asignado manualmente.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Verifica si hay objetivos cercanos y activa/desactiva
        EnfocarParticulasEnObjetivoCercano();
    }

    void EnfocarParticulasEnObjetivoCercano()
    {
        Collider2D[] objetosEnRango = Physics2D.OverlapCircleAll(transform.position, rango, 1 << 8);

        if (objetosEnRango.Length > 0)
        {
            // Hay objetivos cercanos, activa el sistema de part�culas
            ActivarParticulas();

            Transform objetivoCercano = BuscarObjetivoCercano(objetosEnRango);

            if (objetivoCercano != null)
            {
                ApuntarParticulasAObjetivo(objetivoCercano.position);
            }
        }
        else
        {
            // No hay objetivos cercanos, desactiva el sistema de part�culas
            DesactivarParticulas();
        }
    }

    void ActivarParticulas()
    {
        if (!sistemaDeParticulas.isPlaying)
        {
            sistemaDeParticulas.Play();
        }
    }

    void DesactivarParticulas()
    {
        if (sistemaDeParticulas.isPlaying)
        {
            sistemaDeParticulas.Stop();
        }
    }

    Transform BuscarObjetivoCercano(Collider2D[] objetos)
    {
        Transform objetivoCercano = null;
        float distanciaMinima = Mathf.Infinity;

        foreach (Collider2D objeto in objetos)
        {
            float distanciaObjeto = Vector2.Distance(transform.position, objeto.transform.position);

            if (distanciaObjeto < distanciaMinima)
            {
                distanciaMinima = distanciaObjeto;
                objetivoCercano = objeto.transform;
            }
        }

        return objetivoCercano;
    }

    void ApuntarParticulasAObjetivo(Vector3 posicionObjetivo)
    {
        // Calcular la direcci�n hacia el objetivo
        Vector3 direccion = posicionObjetivo - transform.position;
        direccion.Normalize();

        // Rotar el objeto para que apunte hacia el objetivo
        float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angulo));

        // Si tienes un sistema de part�culas, puedes asignar la rotaci�n a su m�dulo de rotaci�n si es necesario
        if (sistemaDeParticulas != null)
        {
            var mainModule = sistemaDeParticulas.main;
            mainModule.startRotationZ = -angulo * Mathf.Deg2Rad;
        }


    }
}