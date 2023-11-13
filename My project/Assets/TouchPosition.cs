using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchPosition : MonoBehaviour
{
    public Vector3 worldPosition;
    private Vector3 currentPos;
    public GameObject debug;
    void Update()
    {
        // Verificar si se ha tocado la pantalla
        if (Input.touchCount > 0)
        {
            // Obtener el primer toque en la pantalla
            Touch touch = Input.GetTouch(0);

            // Verificar si el toque está en la fase de inicio (cuando el dedo toca la pantalla)
            if (touch.phase == TouchPhase.Began)
            {
                // Obtener la posición del toque en coordenadas de pantalla
                Vector3 touchPosition = touch.position;

                // Convertir la posición de la pantalla a coordenadas del mundo
                worldPosition =new Vector3( Camera.main.ScreenToWorldPoint(touchPosition).x, Camera.main.ScreenToWorldPoint(touchPosition).y,0);

                // Ahora worldPosition contiene la posición en el mundo 2D donde se tocó la pantalla
               
                debug.transform.position = worldPosition;
                currentPos = worldPosition;
                // Puedes realizar otras acciones con la posición obtenida, como mover un objeto a esa posición, etc.
            }
        }
        else
        {
            //debug.transform.position =currentPos ;
        }
        
       
    }
    
}



