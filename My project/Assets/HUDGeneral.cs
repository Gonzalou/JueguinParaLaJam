using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUDGeneral : MonoBehaviour
{
    public GameObject LoseHud;
    public GameObject WinHud;
    public TextMeshProUGUI totalpipol;
    public pipolCounter pp;
    
    public Lider ld;
    // Start is called before the first frame update
    void Start()
    {
        WinHud.SetActive(false);
        LoseHud.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Level1");
        }
        if (ld.llegue)
        {
            if (pp.pipolCount < 15)
            {
                LoseHud.SetActive(true);
            }
            WinHud.SetActive(true);
            totalpipol.text = (pp.pipolCount+pp.repres.Length+1) + " personas se presentaron frente a la Casa de Gobierno!";
        }
    }
    public void playAgain()
    {
        SceneManager.LoadScene("Level1");
    }
    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
   
}
