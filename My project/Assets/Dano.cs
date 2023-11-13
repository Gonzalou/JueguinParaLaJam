using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dano : MonoBehaviour
{
    private AudioSource Audio;
    public AudioClip[] sonidosDeDaño;
    public AudioClip sonidoDeMuerte;
    public ParticleSystem pS;



    public int life;
    // Start is called before the first frame update

    private bool canMove = true;
    void Start()
    {
        Audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Si la entidad no puede moverse, retorna sin hacer nada
        if (!canMove)
        {
            return;
        }

        // Aquí deberías tener el código que controla el movimiento de la entidad.
        // Puedes mover la entidad en función de su vida o cualquier otro parámetro.
    }

    private void OnCollisionEnter2D(Collision2D collision)

    {
        if (collision.gameObject.layer == 12)
        {
            life -= 1;

            if (!Audio.isPlaying)
            {
                Audio.clip = sonidosDeDaño[Random.Range(0, sonidosDeDaño.Length - 1)];
                Audio.Play();
                pS.Play();
            }
            if (life <= 0)
            {
                    // Detener el movimiento al llegar a cero vida
                    canMove = false;

                    Audio.clip = sonidoDeMuerte;

            }
        }
    }
}
