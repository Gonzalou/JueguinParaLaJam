using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public float repulsion;
    private Vector2 myDir;
    public Rigidbody2D rb2d;
    public int speed;
    public Dano target;
    public int life;
    public int rangoDeAtaque;
    public int DetectionRange;
    public Animator anim;
    public AudioSource au;
    public AudioClip[] clips;
    public float tiempomuerto;
    public int InitialLife;
    public bool stuned;
    public float tiempoStuneado;
    public ParticleSystem stunParts;
    public bool seHizoGay;
    public Enemigo gayTarget;

    private SpriteRenderer spr;
    // Start is called before the first frame update
    void Start()
    {
        stunParts.Stop();
        spr = GetComponent<SpriteRenderer>();
        InitialLife = life;
        speed = Random.Range(5, 8);
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (myDir.x > 0)
        {
            spr.flipX = true;
        }
        else { spr.flipX = false; }




        if (life <= 0) // mientras está muertito
        {
            tiempomuerto += Time.deltaTime;
            ObtenerDireccion();

            Vector2 limitedVelocity = -myDir * speed;
            rb2d.velocity = limitedVelocity;
        }

        if (tiempomuerto > 7) { life = InitialLife; tiempomuerto = 0; }


        if (tiempoStuneado > 0)
        {
            if (!stunParts.isPlaying)
            {
                stunParts.Play();

            }
            tiempoStuneado -= Time.deltaTime;
        }

        else
        {
            stunParts.Stop(true);
            if (target != null && target.life > 0)  // pasa si está vivo y tiene objetivo
            {
                if (Vector3.Distance(target.transform.position, transform.position) > rangoDeAtaque)
                {
                    ObtenerDireccion();

                    // Calcular la nueva velocidad limitada
                    Vector2 limitedVelocity = myDir * speed;

                    // Aplicar la nueva velocidad
                    rb2d.velocity = limitedVelocity;

                    Vector2 repulsionDirection = (transform.position - target.transform.position).normalized;
                    rb2d.AddForce(repulsionDirection * repulsion);
                }
                else
                {
                    // Calcular la dirección de la repulsión
                    Vector2 repulsionDirection = (transform.position - target.transform.position).normalized;

                    // Aplicar la fuerza de repulsión
                    rb2d.AddForce(repulsionDirection * repulsion);
                }
            }
            else
            {


                // Opcional: Ajustar la velocidad a un valor específico si es necesario
                // rb2d.velocity = new Vector2(0f, 0f);
                if (!seHizoGay)
                {

                    Collider2D[] gente = Physics2D.OverlapCircleAll(transform.position, DetectionRange, 1 << 8);
                    if (gente.Any())
                    {
                        float distanciaMinima = float.MaxValue;
                        for (int i = 0; i < gente.Length; i++)
                        {
                            float distancia = Vector3.Distance(gente[i].transform.position, transform.position);

                            if (distancia < distanciaMinima)
                            {
                                target = gente[i].GetComponent<Dano>();
                                distanciaMinima = distancia;
                            }
                        }
                    }

                }
                else
                {
                    Collider2D[] gente = Physics2D.OverlapCircleAll(transform.position, DetectionRange, 1 << 12);
                    if (gente.Any())
                    {
                        float distanciaMinima = float.MaxValue;
                        for (int i = 0; i < gente.Length; i++)
                        {
                            float distancia = Vector3.Distance(gente[i].transform.position, transform.position);

                            if (distancia < distanciaMinima)
                            {
                                gayTarget = gente[i].GetComponent<Enemigo>();
                                distanciaMinima = distancia;
                            }
                        }
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

            // Calcular la dirección de la repulsión al colisionar
            Vector2 repulsionDirection = (transform.position - collision.transform.position).normalized;

            float repulsionDistanceMultiplier = 10.0f;  // Ajustar este valor si es necesario
            Vector2 repulsionForce = repulsionDirection * repulsion * repulsionDistanceMultiplier;

            // Aplicar la fuerza de repulsión al colisionar
            //rb2d.AddForce(repulsionDirection * repulsion);

            // Aplicar la fuerza de repulsión al colisionar
            rb2d.AddForce(repulsionForce);

            // Ajustar la velocidad después de aplicar la fuerza
            LimitarVelocidad();


        }
        if (collision.gameObject.layer == 12 && seHizoGay)
        {
            anim.SetBool("Atacando", true);
            Enemigo e = collision.gameObject.GetComponent<Enemigo>();
            e.MeHacenDano(3);
            e.tiempoStuneado += 0.5f;
        }
    }
    // Método para limitar la velocidad del Rigidbody2D
    private void LimitarVelocidad()
    {
        float maxSpeed = 5f;  // Ajusta este valor según sea necesario

        // Limitar la velocidad en el eje X y Y por separado
        rb2d.velocity = new Vector2(
            Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed),
            Mathf.Clamp(rb2d.velocity.y, -maxSpeed, maxSpeed)
        );
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            anim.SetBool("Atacando", false);
        }
    }

    public void MeHacenDano(int dmg)
    {
        if (life > 0)
        {
            life -= dmg;
            au.clip = clips[1];
            if (!au.isPlaying)
            {
                au.Play();
            }
        }
    }

    public void ObtenerDireccion()
    {
        if (seHizoGay)
        {
            myDir = (new Vector2(gayTarget.transform.position.x, gayTarget.transform.position.y) - new Vector2(transform.position.x, transform.position.y)).normalized;
        }
        myDir = (new Vector2(target.transform.position.x, target.transform.position.y) - new Vector2(transform.position.x, transform.position.y)).normalized;
    }
}
