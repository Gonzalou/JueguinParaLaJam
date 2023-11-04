using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DOrder : MonoBehaviour
{
    public SpriteRenderer renderer;
   
    public int layerCount;
    // Update is called once per frame
    private void Awake()
    {
        if(gameObject.GetComponent<SpriteRenderer>() != null)
        {
            renderer = gameObject.GetComponent<SpriteRenderer>();
        }
    }

    void Update()
    {
        if (renderer.sprite.name == ("sombra"))
        {
            renderer.sortingOrder = -(int)transform.position.y-1;
            layerCount = renderer.sortingOrder;
        }
        else
        {
            renderer.sortingOrder = -(int)transform.position.y;
            layerCount = renderer.sortingOrder;
        }
     


    }
}
