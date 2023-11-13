using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class hidrantemov : MonoBehaviour
{
    public float speed;
    
    public bool esDerecha; //permite que se mueva a derecha

    public float contadorT; //contador      

    public float cambiodire = 30f; //tiempo que demora en cambiar de dirección


    // Start is called before the first frame update
    void Start()
    {
        contadorT = cambiodire;

    }

    // Update is called once per frame
    void Update()
    {
     
        
        if (esDerecha == true)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
      
        if (esDerecha == false)
        {
            transform.position += Vector3.left* speed * Time.deltaTime;
        }
        
        //esta parte es el contador en funcionamiento, a la inversa y reseteo del contador YA TE DIJE QUE ERA CONTADOR? no me kemeeee
        
        contadorT -= Time.deltaTime;

        if (contadorT <= 0)
        {
            contadorT = cambiodire; 
            esDerecha = !esDerecha;
        }


    }
        
}
