using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public int speed;
    public Dano target;
    public int life;
    public int rangoDeAtaque;
    public int DetectionRange;
    public Animator anim;
    public AudioSource au;
    public AudioClip[] clips;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (life<=0)
        {
            transform.position -= (target.transform.position - transform.position).normalized * speed *1.3f * Time.deltaTime;
        }
        if (target != null&&target.life>0)
        {
            if (Vector3.Distance(target.transform.position, transform.position) > rangoDeAtaque)
                transform.position += (target.transform.position - transform.position).normalized * speed * Time.deltaTime;

            else
            {
                transform.position -= (target.transform.position - transform.position).normalized * speed * 2 * Time.deltaTime;
            }
        }
        else
        {

            Collider2D[] gente = Physics2D.OverlapCircleAll(transform.position, DetectionRange, 1 << 8);
            if (gente.Any())
            {
                float distanciaMinima = float.MaxValue;
                for (int i = 0; i < gente.Length; i++)
                {
                    // Calcular la distancia entre la posición objetivo y la transformación actual
                    float distancia = Vector3.Distance(gente[i].transform.position, transform.position);

                    // Si la distancia actual es menor que la distancia mínima encontrada hasta ahora
                    // Actualizar el índice del transform más cercano y la distancia mínima
                    if (distancia < distanciaMinima)
                    {
                        target = gente[i].GetComponent<Dano>();
                        distanciaMinima = distancia;
                    }
                }

            }



        }
    }

    public void Golpeando()
    {

        if (!au.isPlaying)
        {
            au.clip = clips[0];
            au.Play();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            anim.SetBool("Atacando", true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            anim.SetBool("Atacando", false);
        }
    }

    public void MeHacenDano(int dmg)
    {
        if (life>0)
        {
            life -= dmg;
            au.clip = clips[1];
            if (!au.isPlaying)
            {
                au.Play();
            }
        }
       
    }

}
