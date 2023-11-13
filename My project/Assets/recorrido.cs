using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recorrido : MonoBehaviour
{
    public float velocidad = 5f; // Ajusta la velocidad según tus necesidades
    public float limiteSuperior = 5f; // Ajusta el límite superior del movimiento
    public float limiteInferior = -5f; // Ajusta el límite inferior del movimiento

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

            // Si alcanza el límite superior, invierte la dirección
            if (transform.position.y > limiteSuperior)
            {
                moviendoArriba = false;
            }
        }
        else
        {
            transform.Translate(0f, -desplazamientoY, 0f);

            // Si alcanza el límite inferior, teletransporta a la posición inicial
            if (transform.position.y < limiteInferior)
            {
                transform.position = new Vector3(transform.position.x, limiteSuperior, transform.position.z);
            }
            else if (transform.position.y > limiteSuperior)
            {
                moviendoArriba = true; // Invierte la dirección si no está teletransportando
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer ==0) 
            
        { velocidad = 0; }
    }
}