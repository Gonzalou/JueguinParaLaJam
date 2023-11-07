using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class MobileMovement : MonoBehaviour, IPointerDownHandler

{
    public Lider ld;
    // Start is called before the first frame update
    void Start()
    {
        ld = GameObject.Find("Lider").GetComponent<Lider>();
    }

    // Update is called once per frame
    void Update()
    {

       
    }
    
      public void OnPointerDown(PointerEventData eventData)
    {

        // Esto se ejecutará cuando se libere el botón
        Debug.Log("Botón liberado");
        // Coloca aquí el código que deseas ejecutar cuando el botón se libera
    }
    public void TouchUp(int buttonID)
    {
        Debug.Log("touched");
        ld.transform.position += Vector3.up  * ld.liderSpeed * Time.deltaTime;
        
    }
    public void TouchDown()
    {

        ld.transform.position += Vector3.down * ld.liderSpeed * Time.deltaTime;

    }
    public void TouchLeft()
    {
        ld.transform.position += Vector3.left * ld.liderSpeed * Time.deltaTime;


    }
    public void TouchRight()
    {
        ld.transform.position += Vector3.right * ld.liderSpeed * Time.deltaTime;
    }

}
