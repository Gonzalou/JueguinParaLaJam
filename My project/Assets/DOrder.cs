using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DOrder : MonoBehaviour
{
    public SpriteRenderer rdr;
   
    public int layerCount;
    // Update is called once per frame
    private void Awake()
    {
        if(gameObject.GetComponent<SpriteRenderer>() != null)
        {
            rdr = gameObject.GetComponent<SpriteRenderer>();
        }
    }

    void Update()
    {
        if (rdr.sprite.name == ("sombra"))
        {
            rdr.sortingOrder = -(int)transform.position.y-1;
            layerCount = rdr.sortingOrder;
        }
        else
        {
            rdr.sortingOrder = -(int)transform.position.y;
            layerCount = rdr.sortingOrder;
        }
     


    }
}
