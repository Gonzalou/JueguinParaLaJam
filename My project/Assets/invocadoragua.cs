using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvocadorAgua : MonoBehaviour
{
    public float rangoDeAtaque = 5f; // Rango de ataque para detectar objetivos
    private ParticleSystem sistemaDeParticulas;
    private bool hayObjetivosCerca = false;

    // Start is called before the first frame update
    void Start()
    {
        // Obtener el componente ParticleSystem del mismo objeto
        sistemaDeParticulas =GetComponentInChildren<ParticleSystem>();

        if (sistemaDeParticulas == null)
        {
            Debug.LogError("Se requiere un componente ParticleSystem en el objeto.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //  activar el sistema de partículas cuando hay objetivos cerca
        if (hayObjetivosCerca && !sistemaDeParticulas.isPlaying)
        {
            ActivarParticulas();
        }
        //  desactivar el sistema de partículas cuando no hay objetivos cerca
        else if (!hayObjetivosCerca && sistemaDeParticulas.isPlaying)
        {
            DesactivarParticulas();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar si el objeto que entró en el rango es un objetivo (puedes ajustar las capas según tu configuración)
        if (other.gameObject.layer == 8) // Capa de objetivos
        {
            hayObjetivosCerca = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Verificar si el objeto que salió del rango es un objetivo
        if (other.gameObject.layer == 8) // Capa de objetivos
        {
            hayObjetivosCerca = false;
        }
    }

    void ActivarParticulas()
    {
        // Verificar que el sistema de partículas no sea nulo y no esté reproduciéndose
        if (sistemaDeParticulas.isPlaying)
        {
            sistemaDeParticulas.Play();
            Debug.Log("Partículas activadas");
        }
    }

    void DesactivarParticulas()
    {
        // Verificar que el sistema de partículas no sea nulo y esté reproduciéndose
        if (sistemaDeParticulas.isPlaying)
        {
            sistemaDeParticulas.Stop();
           
            Debug.Log("Partículas desactivadas");
        }
    }
}