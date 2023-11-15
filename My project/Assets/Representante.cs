using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Representante : MonoBehaviour
{
    //Cosas de comportamiento rebaño
    public float velocidadMaxima = 5f;
    public float radioVecindad = 1.5f;
    public float fuerzaSeparacion = 1f;
    public float fuerzaCohesion = 1f;
    public float fuerzaAlineacion = 1f;
    public float separacionMinimaDelTarget;
    //--------------------------------


    public Rigidbody2D rb2d;
    public Lider lider;
    public MousePosition mousePosition;
    public GameObject highLight;
    public int myIndex;
    public float speed;
    public bool canMove;

    public bool agarrado;
    public bool MouseSOBREMI;


    public SpriteRenderer spr;
    public Color[] colors;


    public bool sumoInfluencia;
    public float myInfluence;
    public Transform myTarget;


    public float maxInfluence;
    public float minInfluence;
    // Start is called before the first frame update
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        myInfluence = Random.Range(minInfluence, maxInfluence);
        myIndex = Random.Range(0, 4);
    }
    void Start()
    {
        spr.color = colors[myIndex];
        lider = GameObject.Find("Lider").GetComponent<Lider>();
        mousePosition = GameObject.Find("Puntero").GetComponent<MousePosition>();
    }

    // Update is called once per frame
    void Update()
    {
        highLight.SetActive(MouseSOBREMI);
        if (Vector3.Distance(lider.transform.position, transform.position) < lider.influenceRadius && !sumoInfluencia) //sistema para agrandar el rango de influencia
        {
            Debug.Log("toma influencia");
            sumoInfluencia = true;
            lider.influenceRadius += myInfluence;
            
        }
        if (Vector3.Distance(transform.position, lider.transform.position) < lider.influenceRadius) //si ta muy lejos de lider ni va
        {
            //-----Sistema de Rebaño hacia myTarget-----------
            BuscarTarget();

            if (canMove && myTarget != null)
            {

                Vector2 separacion = Separacion();
                Vector2 cohesion = Cohesion();
                Vector2 alineacion = Alineacion();

                // Combina las fuerzas para obtener el movimiento resultante
                Vector2 movimiento = separacion + cohesion + alineacion;
                movimiento = Vector2.ClampMagnitude(movimiento, velocidadMaxima);

                // Aplica el movimiento a la entidad
                rb2d.velocity += (movimiento * Time.deltaTime);

                if (Vector3.Distance(transform.position, lider.transform.position) >= separacionMinimaDelTarget)
                {
                    rb2d.velocity += (new Vector2(myTarget.transform.position.x, myTarget.transform.position.y) - new Vector2(transform.position.x, transform.position.y)).normalized * speed;
                }
                
            }


            if (lider.Hre.HudOn)
            {
                if (Input.GetMouseButton(0) && MouseSOBREMI && !mousePosition.mouseOcupado)//ACA LO AGARRO
                {
                    mousePosition.mouseOcupado = true;
                    canMove = false;
                    agarrado = true;

                }

                if (agarrado)// Sigue al mouse
                {
                    Onagarrado();
                }
                if (Input.GetMouseButtonUp(0)&&agarrado)//ACA LO SUELTO SI PUEDO
                {
                    if (myTarget == null)
                    {
                        BuscarTarget();
                    }
                    Debug.Log("puedo soltarlo y va a buscar lugar");
                    mousePosition.mouseOcupado = false;
                    agarrado = false;
                    canMove = true;


                }

            }
        }

    }

    public void BuscarTarget()
    {
        Transform transformMasCercano = null;
        float distanciaMasCercana = Mathf.Infinity;
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, 30, 1 << 7);
        foreach (Collider2D t in targets)
        {
            // Calcula la distancia entre el objeto actual y el objetivo
            float distanciaAlObjetivo = Vector3.Distance(t.transform.position, transform.position);

            // Compara si la distancia actual es menor que la distancia más cercana registrada
            if (distanciaAlObjetivo < distanciaMasCercana)
            {
                // Actualiza el objeto más cercano y la distancia más cercana
                transformMasCercano = t.transform;
                distanciaMasCercana = distanciaAlObjetivo;
            }
        }
        myTarget = transformMasCercano;
    }

    public void Onagarrado()
    {
        transform.position = mousePosition.transform.position;
        myTarget = null;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (lider.Hre.HudOn)
        {
            if (collision.gameObject.layer == 3)// si estoy tocando el mouse
            {
                MouseSOBREMI = true;
            }


            if (collision.gameObject.layer == 9)
            {
                //  collision.gameObject.GetComponent<Representante>();
            }
        }

    }


    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.layer == 3)
        {
            MouseSOBREMI = false;
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
}
