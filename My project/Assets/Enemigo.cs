using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    // Start is called before the first frame update
    void Start()
    {
        InitialLife = life;
        speed = Random.Range(5, 8);
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (life <= 0) // mientras est� muertito
        {
            tiempomuerto += Time.deltaTime;
            ObtenerDireccion();

            Vector2 limitedVelocity = -myDir * speed;
            rb2d.velocity = limitedVelocity;
        }

        if (tiempomuerto > 7) { life = InitialLife; tiempomuerto = 0; }
        if (target != null && target.life > 0)  // pasa si est� vivo y tiene objetivo
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
                // Calcular la direcci�n de la repulsi�n
                Vector2 repulsionDirection = (transform.position - target.transform.position).normalized;

                // Aplicar la fuerza de repulsi�n
                rb2d.AddForce(repulsionDirection * repulsion);
            }
        }
        else
        {
            // Despu�s de eliminar al objetivo, detener la aplicaci�n de la fuerza de repulsi�n
            rb2d.velocity = new Vector2(6f, 6f);

            // Opcional: Ajustar la velocidad a un valor espec�fico si es necesario
            // rb2d.velocity = new Vector2(0f, 0f);

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

            // Calcular la direcci�n de la repulsi�n al colisionar
            Vector2 repulsionDirection = (transform.position - collision.transform.position).normalized;

            float repulsionDistanceMultiplier = 15000.0f;  // Ajustar este valor si es necesario
            Vector2 repulsionForce = repulsionDirection * repulsion * repulsionDistanceMultiplier;

            // Aplicar la fuerza de repulsi�n al colisionar
            //rb2d.AddForce(repulsionDirection * repulsion);

            // Aplicar la fuerza de repulsi�n al colisionar
            rb2d.AddForce(repulsionForce);

            // Ajustar la velocidad despu�s de aplicar la fuerza
            LimitarVelocidad();


        }
    }
    // M�todo para limitar la velocidad del Rigidbody2D
    private void LimitarVelocidad()
    {
        float maxSpeed = 5f;  // Ajusta este valor seg�n sea necesario

        // Limitar la velocidad en el eje X y Y por separado
        rb2d.velocity = new Vector2(
            Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed),
            Mathf.Clamp(rb2d.velocity.y, -maxSpeed, maxSpeed)
        );
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
        myDir = (new Vector2(target.transform.position.x, target.transform.position.y) - new Vector2(transform.position.x, transform.position.y)).normalized;
    }
}
