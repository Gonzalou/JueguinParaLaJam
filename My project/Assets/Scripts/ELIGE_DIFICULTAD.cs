using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ELIGE_DIFICULTAD : MonoBehaviour
{
    [SerializeField] private Button facilButton;
    [SerializeField] private Button medioButton;
    [SerializeField] private Button dificilButton;

    // Start is called before the first frame update
    void Start()
    {
        // Asignar funciones a los botones
        facilButton.onClick.AddListener(() => OnButtonClick("facil"));
        medioButton.onClick.AddListener(() => OnButtonClick("medio"));
        dificilButton.onClick.AddListener(() => OnButtonClick("dificil"));
    }

    // Update is called once per frame
    void Update()
    {
        // Puedes agregar lógica de actualización si es necesario
    }




    void OnButtonClick(string dificultad)
    {
        // Asigna la dificultad seleccionada
        GameManager.dificultad = dificultad;

        // Carga la escena "ELIJA_RECLAMO"
        SceneManager.LoadScene("ELIGE_RECLAMO");

    }




}
