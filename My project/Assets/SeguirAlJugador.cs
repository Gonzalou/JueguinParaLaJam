using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeguirAlJugador : MonoBehaviour
{   
        public Transform jugador;
        public float suavizado = 0.5f;
        public Vector3 offset;

        void LateUpdate()
        {
            if (jugador != null)
            {
                Vector3 nuevaPosicion = jugador.position + offset;
                Vector3 posicionSuavizada = Vector3.Lerp(transform.position, nuevaPosicion, suavizado*Time.deltaTime);
                transform.position = new Vector3(posicionSuavizada.x, posicionSuavizada.y, transform.position.z);
            }
        }    
}
