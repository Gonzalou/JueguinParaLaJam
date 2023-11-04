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
    public float influencePower;
    public int myIndex;
    public float speed;
    public bool canMove;
    public bool canPlace;
    public bool agarrado;
    public bool MouseSOBREMI;
    // Start is called before the first frame update
    void Start()
    {
        previousReprePos = myReprePosition;
        lider = GameObject.Find("Lider").GetComponent<Lider>();
        mousePosition = GameObject.Find("Puntero").GetComponent<MousePosition>();
    }

    // Update is called once per frame
    void Update()
    {

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
            if (Input.GetMouseButton(0) && MouseSOBREMI)//ACA LO AGARRO
            {

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
                ElegirMiPosicion();
                agarrado = false;
                canMove = true;
                canPlace = false;
            }
            else  //no lo puedo soltar pero deje de apretar clic
            {
                if (Input.GetMouseButtonUp(0))
                {
                    myReprePosition = previousReprePos;

                    canMove = true;
                    canPlace = false;
                }
                
            }
        }

    }

    public void ElegirMiPosicion()
    {

        Debug.Log("elijo Mi lugar");
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
                myReprePosition= i;
                distanciaMinima = distancia;
            }
        }
      
        Debug.Log(myReprePosition.ToString("es mi lugar"));
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
                Debug.Log(collision.name);
                MouseSOBREMI = true;

            }

            if (collision.gameObject.layer == 7 && agarrado) //lo puedo reposisionar
            {
                Debug.Log(collision.name);
                canPlace = true;
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
            Debug.Log(collision.name+("sali de"));
            canPlace = false;

        }



    }
}
