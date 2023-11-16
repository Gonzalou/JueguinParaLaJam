using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogicaBarra : MonoBehaviour
{
    public LayerMask capaProtestante;
    public float velocidadDecremento = 1.0f;
    public float valorDeDecremento = 1;
    public float velocidadIncremento = 1.0f;
    public float valorMaximo = 100.0f;
    public Lider ld;
    public float valorActual;
    private Slider barraInfluencia;
    private List<Dano> nPCs = new List<Dano>();


    public int valorPorNpc;

    

    void Start()
    {
        ld = GameObject.Find("Lider").GetComponent<Lider>();
        barraInfluencia = GetComponent<Slider>();

        if (barraInfluencia == null)
        {
            Debug.LogError("Este script deber�a estar en un objeto con un componente Slider adjunto.");
        }

        valorActual = valorMaximo;
    }

    void Update()
    {
        // bajar la barra de influencia si no hay objetos de la capa "Protestante" cerca
        bool hayProtestantesCerca = HayProtestantesCerca();
        velocidadDecremento = valorDeDecremento / nPCs.Count;

        if (velocidadDecremento < 1.8)
        {
            velocidadDecremento = 1.8f;
        }
        DecrementarBarra();
        

    }

    bool HayProtestantesCerca()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(ld.transform.position, ld.influenceRadius, 1 << 8);

        foreach (Collider2D c in colliders)
        {
            Dano p = c.gameObject.GetComponent<Dano>();
            if (p.life > 0 && !nPCs.Contains(p))
            {

                nPCs.Add(p);
                IncrementarBarra();
            }
            if (nPCs.Contains(p) && p.life < 0)
            {
                nPCs.Remove(p);
            }
        }
        bool hayProtestantes = colliders.Length > 1;

        if (hayProtestantes)
        {
       
        }
        else
        {
          
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
        /* valorActual += velocidadIncremento * Time.deltaTime;
         valorActual = Mathf.Clamp(valorActual, 0.0f, valorMaximo);*/
        valorActual += valorPorNpc;
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
