using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class GameManager : MonoBehaviour
{
    public static string dificultad;
    public static string miRepresentante;
    public static string reclamo;

}


public class MainMenu : MonoBehaviour
{
    public GameObject controlsUI;
    public GameObject buttonsParent;
    public GameObject optionsParent;

    private float tiempito;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tiempito += Time.deltaTime;
        if (Input.anyKeyDown&&tiempito>5)
        {
            PlayButton();
        }
    }

    public void PlayButton()
    {
        //SceneManager.LoadScene("Level1");
        SceneManager.LoadScene("oyarce2");
    }
    public void creditsButton()
    {
        SceneManager.LoadScene("Credits");
    }
    public void OptionsButton()
    {
        optionsParent.SetActive(true);


    }
    public void ShowControls()
    {
        controlsUI.gameObject.SetActive(true);
        buttonsParent.SetActive(false);
    }
}
