using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject controlsUI;
    public GameObject buttonsParent;
    public GameObject optionsParent;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("Level1");
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
