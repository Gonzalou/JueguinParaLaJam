using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Representante : MonoBehaviour
{
    //Velocidade
    public float velocidadMaxima = 5f;
    public float separacionMinimaDelTarget;
    public float speed;
    //--------------------------------


    public Rigidbody2D rb2d;
    public Lider lider;
    public MousePosition mousePosition;
    public GameObject highLight;
    public int myIndex;
    public bool canMove;

    public bool agarrado;
    public bool MouseSOBREMI;


    public SpriteRenderer spr;  


    public bool sumoInfluencia;
    public float myInfluence;
    public PlataformaR myTarget;


    public float maxInfluence;
    public float minInfluence;

    public bool habilidadOn=false;
    public Button myButton;
    
    // Start is called before the first frame update
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        myInfluence = Random.Range(minInfluence, maxInfluence);
        UnlockSkill();
    }
    void Start()
    {        
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

            if (myTarget == null)
            {
                BuscarTarget();
            }
            LimitarVelocidad();
            if (canMove && myTarget != null)
            {

                // Aplica el movimiento a la entidad
                habilidadOn = true;
                
                

                if (Vector3.Distance(transform.position, lider.transform.position) >= separacionMinimaDelTarget)
                {
                    rb2d.velocity += (new Vector2(myTarget.transform.position.x, myTarget.transform.position.y) - new Vector2(transform.position.x, transform.position.y)).normalized * speed*Time.deltaTime;
                }
                else
                {
                    rb2d.velocity = Vector2.zero;
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
                if (Input.GetMouseButtonUp(0) && agarrado)//ACA LO SUELTO SI PUEDO
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
        else
        {
            habilidadOn = false;    

        }

    }

    private void LateUpdate()
    {
        myButton.interactable = habilidadOn;
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
            if (distanciaAlObjetivo < distanciaMasCercana && t.GetComponent<PlataformaR>().repreInMe == 0)
            {
                // Actualiza el objeto más cercano y la distancia más cercana
                transformMasCercano = t.transform;
                distanciaMasCercana = distanciaAlObjetivo;
            }
        }
        if (transformMasCercano != null)
        {
            myTarget = transformMasCercano.GetComponent<PlataformaR>();

            myTarget.repreInMe = myIndex;
        }
    }

    public void Onagarrado()
    {
        transform.position = mousePosition.transform.position;
        myTarget.repreInMe = 0;
        myTarget = null;

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

    public void UnlockSkill()
    {
      myButton= GameObject.Find("Habilidad "+myIndex).GetComponent<Button>();
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





}
