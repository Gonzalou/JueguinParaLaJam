using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Menuinicio : MonoBehaviour
{
    public GameObject eligeDificultad;
    public GameObject eligeReclamo;
    public GameObject eligeRrepresentante;
    public string Representante;
    public string Dificultad;
    public string Reclamo;

    void Start()
    {
        RepresentantePanel();
        
    }


    void RepresentantePanel()
    {
        // Oculta los objetos al iniciar la escena
        eligeDificultad.SetActive(false);
        eligeReclamo.SetActive(false);
        eligeRrepresentante.SetActive(true);
    }

void ReclamoPanel()
    {
        // Oculta los objetos al iniciar la escena
        eligeDificultad.SetActive(false);
        eligeReclamo.SetActive(true);
        eligeRrepresentante.SetActive(false);

    }

void DificultadPanel()
    {
        // Oculta los objetos al iniciar la escena
        eligeDificultad.SetActive(true);
        eligeReclamo.SetActive(false);
        eligeRrepresentante.SetActive(false);
                Debug.Log("Botón presionado");
        
    }


    void IniciarJuego(){
    SceneManager.LoadScene("Level1");
}


}