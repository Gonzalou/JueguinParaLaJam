using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotonHabilidad : MonoBehaviour
{
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
                Debug.Log("Habilidad lista");
            }
        }
        // Verificador de la tecla "1" presionada, activa la habilidad si está lista
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ActivarHabilidad();
        }
    }

    public void ActivarHabilidad()
    {
        if (estaDisponible)
        {
            Debug.Log("Habilidad activada wachin");
            estaDisponible = false;
        }
        else
        {
            Debug.Log("Habilidad en cooldown baja un cambio manija" + tiempoRestante.ToString("F1") + " segundos.");
        }
    }
}