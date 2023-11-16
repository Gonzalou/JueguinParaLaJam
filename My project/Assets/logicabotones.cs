using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotonHabilidad : MonoBehaviour
{
    public int numeroHabilidad = 1; // Número de la habilidad asociada a este botón
    public float cooldown = 5.0f;
    private float tiempoRestante = 0.0f;
    private bool estaDisponible = true;

    void Start()
    {
        tiempoRestante = cooldown;
    }

    void Update()
    {
        if (!estaDisponible)
        {
            tiempoRestante -= Time.deltaTime;

            if (tiempoRestante <= 0.0f)
            {
                estaDisponible = true;
                tiempoRestante = cooldown;
                Debug.Log("Habilidad " + numeroHabilidad + " lista");
            }
        }

        // Verifica la tecla asociada a la habilidad y activa la habilidad si está lista
        if (Input.GetKeyDown(KeyCode.Alpha0 + numeroHabilidad))
        {
            ActivarHabilidad();
        }
    }

    public void ActivarHabilidad()
    {
        if (estaDisponible)
        {
            Debug.Log("Habilidad " + numeroHabilidad + " activada wachin");
            estaDisponible = false;
        }
        else
        {
            Debug.Log("Habilidad " + numeroHabilidad + " en cooldown baja un cambio manija " + tiempoRestante.ToString("F1") + " segundos.");
        }
    }
}