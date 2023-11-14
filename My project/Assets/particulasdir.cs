using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particulasdir : MonoBehaviour
{

    public float rango = 40f; // Rango de detecci�n
    public ParticleSystem sistemaDeParticulas; // Referencia al sistema de part�culas

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        EnfocarParticulasEnObjetivoCercano();
    }

    void EnfocarParticulasEnObjetivoCercano()
    {
        Collider2D[] objetosEnRango = Physics2D.OverlapCircleAll(transform.position, rango, 1<<8);

        if (objetosEnRango.Length > 0)
        {
            sistemaDeParticulas.Play();
            Transform objetivoCercano = BuscarObjetivoCercano(objetosEnRango);

            if (objetivoCercano != null)
            {
                ApuntarParticulasAObjetivo(objetivoCercano.position);
            }
        }

        else
        {
            sistemaDeParticulas.Stop();
        }
    }

    Transform BuscarObjetivoCercano(Collider2D[] objetos)  //TODO ESTO EL LA PRIORIDAD DE APUNTADO DEL CHORRO
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

    void ApuntarParticulasAObjetivo(Vector3 posicionObjetivo)  //TODO ESTO ES EL MOVIMIENTO APUNTADO DEL CHORRO
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