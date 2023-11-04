using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dano : MonoBehaviour
{
    private AudioSource Audio;
    public AudioClip sonidoDeDaño;
    public AudioClip sonidoDeMuerte;
   
    public int life;
    // Start is called before the first frame update
    void Start()
    {
        Audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer==12)
        {
            Audio.clip = sonidoDeDaño;
            Audio.Play();
            life -= 1;
        }
        if (life<=0)
        {
            Audio.clip=sonidoDeMuerte;         

        }
    }
}
