using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Representante : MonoBehaviour
{
    public Lider lider;
    // Start is called before the first frame update
    void Start()
    {
        lider=GameObject.Find("Lider").GetComponent<Lider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
