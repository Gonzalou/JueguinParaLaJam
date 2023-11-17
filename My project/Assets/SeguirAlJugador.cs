using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeguirAlJugador : MonoBehaviour
{
    public Transform jugador;
    public float suavizado = 0.5f;
    public Vector3 offset;
    private Rigidbody2D rb;
    void LateUpdate()
    {
        if (jugador != null)
        {
            Vector3 nuevaPosicion = jugador.position + offset;
            Vector3 posicionSuavizada = Vector3.Lerp(transform.position, nuevaPosicion, suavizado * Time.deltaTime);
            transform.position = new Vector3(posicionSuavizada.x, posicionSuavizada.y, transform.position.z);
            rb = transform.GetComponent<Rigidbody2D>();
            if (rb != null)
            {

                if (rb.velocity.magnitude > 1)
                {
                    if (rb.velocity.x < 0)
                    {
                        Vector3.Lerp(offset, new Vector3(-30, offset.y, offset.z), Time.deltaTime);

                    }
                    if (rb.velocity.y > 0)
                    {
                        Vector3.Lerp(offset, new Vector3(30, offset.y, offset.z), Time.deltaTime);
                    }
                }
            }

        }
    }
}
