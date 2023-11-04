using UnityEngine;
using UnityEngine.UIElements;

public class Representante : MonoBehaviour
{
    public Lider lider;
    public MousePosition mousePosition;
    public int myReprePosition;
    public float speed;
    public bool canMove;
    public bool canPlace;
    public bool agarrado;
    // Start is called before the first frame update
    void Start()
    {
        lider = GameObject.Find("Lider").GetComponent<Lider>();
        mousePosition= GameObject.Find("Puntero").GetComponent<MousePosition>();
    }

    // Update is called once per frame
    void Update()
    {


        if (canMove)
        {
            if (myReprePosition <=4)
            {
                transform.position += ( lider.reprePositions[myReprePosition].position - transform.position).normalized * speed * Time.deltaTime;
                
            }
        }
        if (agarrado )// Sigue al mouse
        {
            Onagarrado();
        }
        if (canPlace && Input.GetMouseButtonDown(0))//ACA LO SUELTO SI PUEDO
        {
            ElegirMiPosicion();
            agarrado = false;
            canMove = true;
            canPlace = false;
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
                distanciaMinima = distancia;
            }
        }
    }

    public void Onagarrado()
    {
        transform.position=mousePosition.transform.position;
    
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)// si estoy tocando el mouse
        {
            Debug.Log(collision.name);
            if (Input.GetMouseButtonDown(0))//ACA LO AGARRO
            {
                canMove = false;
                agarrado = true;
                myReprePosition = 5;
            }
        }

        if (collision.gameObject.layer == 7 && agarrado) //lo puedo reposisionar
        {
            canPlace = true;           
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7 && agarrado)
        {
            if (canPlace && Input.GetMouseButtonUp(0))//ACA LO SUELTO y vuelve a su posicion
            {               
                agarrado = false;
                canMove = true;
            }
        }
    }
}
