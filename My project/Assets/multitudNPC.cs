
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class multitudNPC : MonoBehaviour
{
    public float velocidadMaxima = 5f;
    public float radioVecindad = 1.5f;
    public float fuerzaSeparacion = 1f;
    public float fuerzaCohesion = 1f;
    public float fuerzaAlineacion = 1f;
    public Lider liderPos;
    public Representante myRepresentant;
    public float VelocidadHaciaElLider;
    public float SeparacionMinimaDelLider;
    public int representanteINDEX;
    public SpriteRenderer spr;
    public Sprite sprMuerto;
    public Animator anim;
    public Transform target;
    public Color[] colors;
    private Rigidbody2D rb2d;

    private bool haciendoDano;

    public float tiempoDeDano = 2f; // Duración del daño en segundos
    public int cantidadDeDano = 10; // Cantidad de daño a aplicar

    private Dano dmg;
    private void Awake()
    {
        dmg = GetComponent<Dano>();
        representanteINDEX = Random.Range(0, 3);        
       rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        liderPos = GameObject.Find("Lider").GetComponent<Lider>();
        spr.color = colors[representanteINDEX];
        switch(representanteINDEX)
        {
            case 0:
                VelocidadHaciaElLider -= 0.5f;
                
                break;
                case 1: VelocidadHaciaElLider = VelocidadHaciaElLider * 0.7f;
                break;
                case 2: VelocidadHaciaElLider = VelocidadHaciaElLider * 1.2f;
                break;
            case 3:
                VelocidadHaciaElLider = VelocidadHaciaElLider * 1.4f;
                break;

        }
    }
    void Update()
    {

        LimitarVelocidad();
        


        if (dmg.life > 0)
        {



            if (liderPos != null)
            {


                if (Vector3.Distance(transform.position, liderPos.transform.position) < liderPos.influenceRadius) //si ta muy lejos de lider ni va
                {
                    if (myRepresentant != null)
                    {
                        Vector2 separacion = Separacion();
                        Vector2 cohesion = Cohesion();
                        Vector2 alineacion = Alineacion();

                        // Combina las fuerzas para obtener el movimiento resultante
                        Vector2 movimiento = separacion + cohesion + alineacion;
                        movimiento = Vector2.ClampMagnitude(movimiento, velocidadMaxima);

                        // Aplica el movimiento a la entidad
                        rb2d.velocity+=(movimiento * Time.deltaTime);

                        if (Vector3.Distance(transform.position, liderPos.transform.position) >= SeparacionMinimaDelLider)
                        {
                         rb2d.velocity  +=  (new Vector2(target.transform.position.x, target.transform.position.y) -new Vector2(transform.position.x,transform.position.y)).normalized * VelocidadHaciaElLider ;
                           
                        }

                    }
                    else
                    {
                        List<Representante> repres = new List<Representante>();
                        repres.AddRange(GameObject.FindObjectsOfType<Representante>());
                        foreach (var item in repres)
                        {
                            if (item.myIndex == representanteINDEX)
                            {
                                myRepresentant = item;
                                target = myRepresentant.transform;
                            }
                        }


                    }

                }
            }


        }
        else
        {
            spr.sprite = sprMuerto;
            spr.color = Color.white;
            anim.speed = 0;
        }

    }

    Vector2 Separacion()
    {
        Vector2 fuerzaSeparacion = Vector2.zero;
        Collider2D[] vecinos = Physics2D.OverlapCircleAll(transform.position, radioVecindad);
        foreach (Collider2D vecino in vecinos)
        {
            if (vecino.gameObject != gameObject)
            {
                Vector2 direccion = transform.position - vecino.transform.position;
                fuerzaSeparacion += direccion.normalized / direccion.magnitude;
            }
        }
        return fuerzaSeparacion.normalized * this.fuerzaSeparacion;
    }

    Vector2 Cohesion()
    {
        Vector2 centroRebano = Vector2.zero;
        int cantidadVecinos = 0;
        Collider2D[] vecinos = Physics2D.OverlapCircleAll(transform.position, radioVecindad);
        foreach (Collider2D vecino in vecinos)
        {
            if (vecino.gameObject != gameObject)
            {
                centroRebano += (Vector2)vecino.transform.position;
                cantidadVecinos++;
            }
        }
        if (cantidadVecinos > 0)
        {
            centroRebano /= cantidadVecinos;
            Vector2 direccion = centroRebano - (Vector2)transform.position;
            return direccion.normalized * this.fuerzaCohesion;
        }
        return Vector2.zero;
    }

    Vector2 Alineacion()
    {
        Vector3 direccionMedia = Vector3.zero;
        int cantidadVecinos = 0;
        Collider2D[] vecinos = Physics2D.OverlapCircleAll(transform.position, radioVecindad);
        foreach (Collider2D vecino in vecinos)
        {
            if (vecino.gameObject != gameObject && vecino.GetComponent<multitudNPC>())
            {
                direccionMedia += ((multitudNPC)vecino.GetComponent(typeof(multitudNPC))).liderPos.transform.position;

                cantidadVecinos++;
            }
        }
        if (cantidadVecinos > 0)
        {
            direccionMedia /= cantidadVecinos;
            return direccionMedia.normalized * this.fuerzaAlineacion;
        }
        return Vector2.zero;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 12&&!haciendoDano)
        {

           var e= collision.gameObject.GetComponent<Enemigo>();
            HacerDano(e);
        }
    }
    void HacerDano(Enemigo e)
    {
        if (e != null)
        {
            StartCoroutine(CausarDanoPorTiempo(e));
        }
    }

    IEnumerator CausarDanoPorTiempo(Enemigo e)
    {
        while (true)
        {
            haciendoDano = false;
            e.life -= 1;
            // Aplicar daño aquí (por ejemplo, reducir la salud del enemigo)
            // ...
            // Esperar un segundo antes de aplicar el siguiente tick de daño
            yield return new WaitForSeconds(1f);
            // Reducir el tiempo de duración
            tiempoDeDano -= 1f;

            // Si el tiempo de duración llega a cero, salir de la corrutina
            if (tiempoDeDano <= 0f)
            {
                haciendoDano = false;
                break;
            }
        }

        // Fin de la corrutina, puedes realizar acciones adicionales aquí si es necesario
        // ...
    }


    private void LimitarVelocidad()
    {
        float maxSpeed = velocidadMaxima;  // Ajusta este valor según sea necesario

        // Limitar la velocidad en el eje X y Y por separado
        rb2d.velocity = new Vector2(
            Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed),
            Mathf.Clamp(rb2d.velocity.y, -maxSpeed, maxSpeed)
        );
    }
}

