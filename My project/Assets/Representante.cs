using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Representante : MonoBehaviour
{
    public Lider lider;
    public MousePosition mousePosition;
    public GameObject highLight;
    public int myReprePosition;
    public int previousReprePos;
    public int myIndex;
    public float speed;
    public bool canMove;
    public bool canPlace;
    public bool agarrado;
    public bool MouseSOBREMI;


    public SpriteRenderer spr;
    public Color[] colors;


    private bool sumoInfluencia;
    public float myInfluence;


   
    public float maxInfluence;
    public float minInfluence;
    // Start is called before the first frame update
    private void Awake()
    {
        myInfluence=Random.Range(minInfluence, maxInfluence);
        myIndex = Random.Range(0, 4);
    }
    void Start()
    {
        spr.color = colors[myIndex];
        myReprePosition = Random.Range(0, 4);
        previousReprePos = myReprePosition;
        lider = GameObject.Find("Lider").GetComponent<Lider>();
        mousePosition = GameObject.Find("Puntero").GetComponent<MousePosition>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector3.Distance(lider.transform.position, transform.position) < mousePosition.liderRadius&&!sumoInfluencia)
        {
            Debug.Log("toma influencia");
            sumoInfluencia = true;
            lider.influenceRadius += myInfluence;
        }
      



        highLight.SetActive(canPlace);
        if (canMove)
        {
            if (myReprePosition != 5)
            {
                transform.position += (lider.reprePositions[myReprePosition].position - transform.position).normalized * speed * Time.deltaTime;

            }
        }
        if (lider.Hre.HudOn)
        {
            if (Input.GetMouseButton(0) && MouseSOBREMI&&!mousePosition.mouseOcupado)//ACA LO AGARRO
            {
                mousePosition.mouseOcupado = true;
                canMove = false;
                agarrado = true;
                previousReprePos = myReprePosition;
                myReprePosition = 5;
            }

            if (agarrado)// Sigue al mouse
            {
                Onagarrado();
            }
            if (canPlace && Input.GetMouseButtonUp(0))//ACA LO SUELTO SI PUEDO
            {
                Debug.Log("puedo soltarlo y va a buscar lugar");
                mousePosition.mouseOcupado = false;
                ElegirMiPosicion();
                agarrado = false;
                canMove = true;
                canPlace = false;
            }
            else  //no lo puedo soltar pero deje de apretar clic
            {
                if (Input.GetMouseButtonUp(0) && myReprePosition != previousReprePos)
                {
                    mousePosition.mouseOcupado = false;
                    Debug.Log("no encontre lugar");
                    myReprePosition = previousReprePos;

                    canMove = true;
                    canPlace = false;
                }

            }
        }

    }

    public void ElegirMiPosicion()
    {


        myReprePosition = -1;
        float distanciaMinima = float.MaxValue;

        // Iterar a través de la lista de transformaciones
        for (int i = 0; i < lider.reprePositions.Length; i++)
        {
            // Calcular la distancia entre la posición objetivo y la transformación actual
            float distancia = Vector3.Distance(lider.reprePositions[i].position, transform.position);

            // Si la distancia actual es menor que la distancia mínima encontrada hasta ahora
            // Actualizar el índice del transform más cercano y la distancia mínima
            if (distancia < distanciaMinima)
            {

                myReprePosition = i;
                previousReprePos = i;


                distanciaMinima = distancia;
            }
        }

    }

    public void Onagarrado()
    {
        transform.position = mousePosition.transform.position;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (lider.Hre.HudOn)
        {
            if (collision.gameObject.layer == 3)// si estoy tocando el mouse
            {
                MouseSOBREMI = true;
            }

            if (collision.gameObject.layer == 7 && agarrado) //lo puedo reposisionar
            {
                canPlace = true;
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
        if (collision.gameObject.layer == 7 && agarrado)
        {
            canPlace = false;

        }



    }
}
