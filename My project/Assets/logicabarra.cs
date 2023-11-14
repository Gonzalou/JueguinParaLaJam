using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogicaBarra : MonoBehaviour
{
    public LayerMask capaProtestante;
    public float velocidadDecremento = 1.0f;
    public float velocidadIncremento = 1.0f;
    public float valorMaximo = 100.0f;

    private float valorActual;
    private Slider barraInfluencia;

    void Start()
    {
        barraInfluencia = GetComponent<Slider>();

        if (barraInfluencia == null)
        {
            Debug.LogError("Este script debería estar en un objeto con un componente Slider adjunto.");
        }

        valorActual = valorMaximo;
    }

    void Update()
    {
        // bajar la barra de influencia si no hay objetos de la capa "Protestante" cerca
        bool hayProtestantesCerca = HayProtestantesCerca();

        if (!hayProtestantesCerca)
        {
            DecrementarBarra();
        }
        else
        {
            IncrementarBarra();
        }
    }

    bool HayProtestantesCerca()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 100f, capaProtestante);
        bool hayProtestantes = colliders.Length > 10;

        if (hayProtestantes)
        {
            Debug.Log("¡Protestantes cerca!");
        }
        else
        {
            Debug.Log("No hay protestantes cerca.");
        }

        return hayProtestantes;
    }

    void DecrementarBarra()
    {
        valorActual -= velocidadDecremento * Time.deltaTime;
        valorActual = Mathf.Clamp(valorActual, 0.0f, valorMaximo);

        ActualizarInterfaz();
    }

    void IncrementarBarra()
    {
        valorActual += velocidadIncremento * Time.deltaTime;
        valorActual = Mathf.Clamp(valorActual, 0.0f, valorMaximo);

        ActualizarInterfaz();
    }

    void ActualizarInterfaz()
    {
        if (barraInfluencia != null)
        {
            barraInfluencia.value = valorActual / valorMaximo;
        }
    }
}
