using System.Collections;
using UnityEngine;

public class MousePosition : MonoBehaviour
{
    Camera mainCamera;
    public Vector3 mousePosition;
    public float liderRadius;
    public Lider lider;
    public float cameraZoomDefault;
    public float zoomIn;
    public float timeSlowed;
    private bool liderbajoelmouse;
    public GameObject representantesHUD;
    private float minidelay;

    public bool mouseOcupado;
    void Start()
    {
        lider = GameObject.Find("Lider").GetComponent<Lider>();
        // Obtén la referencia a la cámara principal
        mainCamera = Camera.main;
        cameraZoomDefault = mainCamera.orthographicSize;
    }

    void Update()
    {
        if (lider.Hre.HudOn)
        {
            minidelay += Time.deltaTime;
        }
        else
        {
            minidelay = 0;
        }
        // Obtén la posición del mouse en pantalla
        mousePosition = Input.mousePosition;

        // Ajusta la posición del mouse al espacio del mundo usando la cámara ortográfica
        mousePosition = mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 0f));


        transform.position = new Vector3(mousePosition.x, mousePosition.y, 0f);
        // Puedes usar la posición del mouse (worldMousePosition) para realizar acciones en tu juego



        if (liderbajoelmouse)
        {
            if (Input.GetMouseButtonDown(0))
            {
                lider.Hre.HudOn = true;
                Time.timeScale = timeSlowed;

                lider.liderSpeed = 0f;

                mainCamera.orthographicSize = zoomIn;
                mainCamera.transform.position = new Vector3(lider.transform.position.x, lider.transform.position.y, mainCamera.transform.position.z);

            }
           
            if (Input.GetMouseButtonDown(0) && minidelay >= 0.06f)
            {
                mainCamera.orthographicSize = cameraZoomDefault;
                Time.timeScale = 1;
                lider.liderSpeed = lider.defaultLiderSpeed;
                liderbajoelmouse = false;
                lider.Hre.HudOn = false;
            }
        }



    }

    private void OnTriggerStay2D(Collider2D collision)
    {


        if (collision.gameObject.layer == 10)
        {
            liderbajoelmouse = true;

            //se abre menu de acomodar representantes            
        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.layer == 10)
        {
            liderbajoelmouse = false;

            //se abre menu de acomodar representantes            
        }
    }
    IEnumerator EsperarPorMenosDeUnSegundo()
    {
        // Espera por menos de un segundo
        yield return new WaitForSeconds(0.5f); // Espera por 0.5 segundos

        // Después de esperar, continúa con el código aquí

    }

}
