using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dano : MonoBehaviour
{
    public AudioSource SonidoDeDa�o;
    public int life;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer==12)
        {
            SonidoDeDa�o.Play();
            life -= 1;
        }
        if (life<=0)
        {
            Destroy(gameObject);
        }
    }
}
