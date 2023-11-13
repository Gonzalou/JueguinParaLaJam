using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recorrido : MonoBehaviour
{
    public float velocidad = 5f; // Ajusta la velocidad según tus necesidades
    public float limiteSuperior = 5f; // Ajusta el límite superior del movimiento
    public float limiteInferior = -5f; // Ajusta el límite inferior del movimiento

    private bool moviendoArriba;

    private float posicionInicialY;

    // Start is called before the first frame update
    void Start()
    {
        posicionInicialY = transform.position.y;

        // Chequea origen del objeto para saber qué dirección tomar
        if (posicionInicialY > -421f)
        {
            moviendoArriba = true; // Inicia moviéndose hacia arriba
        }
        else
        {
            moviendoArriba = false; // Inicia moviéndose hacia abajo
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Calcula el desplazamiento en el eje Y
        float desplazamientoY = velocidad * Time.deltaTime;

        // Mueve el objeto a lo largo del eje Y
        if (moviendoArriba)
        {
            transform.Translate(0f, desplazamientoY, 0f);

            // Si alcanza el límite superior, teletransporta a la posición inicial y cambia la dirección a hacia abajo
            if (transform.position.y > limiteSuperior)
            {
                if (posicionInicialY > -421f)
                {
                    transform.position = new Vector3(transform.position.x, posicionInicialY, transform.position.z);
                }
                else
                {
                    transform.position = new Vector3(transform.position.x, -421f, transform.position.z);
                }
                moviendoArriba = false;
            }
        }
        else
        {
            transform.Translate(0f, -desplazamientoY, 0f);

            // Si alcanza el límite inferior, teletransporta a la posición inicial y cambia la dirección a hacia arriba
            if (transform.position.y < limiteInferior)
            {
                if (posicionInicialY > -421f)
                {
                    transform.position = new Vector3(transform.position.x, posicionInicialY, transform.position.z);
                }
                else
                {
                    transform.position = new Vector3(transform.position.x, -421f, transform.position.z);
                }
                moviendoArriba = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 0)
        {
            velocidad = 0;
        }
    }
}