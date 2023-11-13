using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recorrido : MonoBehaviour
{
    public float velocidad = 5f; // Ajusta la velocidad seg�n tus necesidades
    public float limiteSuperior = 5f; // Ajusta el l�mite superior del movimiento
    public float limiteInferior = -5f; // Ajusta el l�mite inferior del movimiento

    private bool moviendoArriba = true;

    // Update is called once per frame
    private void Start()
    {
        limiteSuperior = transform.position.y;
    }
    void Update()
    {
        // Calcula el desplazamiento en el eje Y
        float desplazamientoY = velocidad * Time.deltaTime;

        // Mueve el objeto a lo largo del eje Y
        if (moviendoArriba)
        {
            transform.Translate(0f, desplazamientoY, 0f);

            // Si alcanza el l�mite superior, invierte la direcci�n
            if (transform.position.y > limiteSuperior)
            {
                moviendoArriba = false;
            }
        }
        else
        {
            transform.Translate(0f, -desplazamientoY, 0f);

            // Si alcanza el l�mite inferior, teletransporta a la posici�n inicial
            if (transform.position.y < limiteInferior)
            {
                transform.position = new Vector3(transform.position.x, limiteSuperior, transform.position.z);
            }
            else if (transform.position.y > limiteSuperior)
            {
                moviendoArriba = true; // Invierte la direcci�n si no est� teletransportando
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer ==0) 
            
        { velocidad = 0; }
    }
}