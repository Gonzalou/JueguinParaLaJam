using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUDGeneral : MonoBehaviour
{
    public Button[] listaBotones;
    public GameObject LoseHud;
    public GameObject WinHud;
    public TextMeshProUGUI totalpipol;
    public pipolCounter pp;
    public LogicaBarra laBarra;
    public Lider ld;
    // Start is called before the first frame update
    void Awake()
    {
        WinHud.SetActive(false);
        LoseHud.SetActive(false);
        Skills myRepre;
        myRepre = (GameObject.FindObjectOfType<Skills>());
        myRepre.buttons = listaBotones;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (ld.llegue)
        {
            if (pp.pipolCount < 15)
            {
                LoseHud.SetActive(true);
            }
            WinHud.SetActive(true);
            totalpipol.text = (pp.pipolCount + pp.repres.Length + 1) + " personas se presentaron frente a la Casa de Gobierno!";
        }

       /* if (laBarra.valorActual <= 0)
        {
            LoseHud.SetActive(true);
           
        }*/
    }
    public void playAgain()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Credits()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Credits");
    }

}
