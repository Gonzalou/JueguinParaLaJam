using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaR : MonoBehaviour
{
    public int repreInMe;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
           repreInMe= collision.GetComponent<Representante>().myIndex;
        }
    }
    
}
