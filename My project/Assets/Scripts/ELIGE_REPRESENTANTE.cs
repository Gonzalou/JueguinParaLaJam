using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ELIGE_REPRESENTANTE : MonoBehaviour
{
    [SerializeField] private Button abuelaButton;
    [SerializeField] private Button academicoButton;
    [SerializeField] private Button gremialistaButton;

    void Start()
    {
        // Asignar funciones a los botones
        abuelaButton.onClick.AddListener(() => OnButtonClick("Abuela"));
        academicoButton.onClick.AddListener(() => OnButtonClick("Academico"));
        gremialistaButton.onClick.AddListener(() => OnButtonClick("Gremialista"));
    }

    void OnButtonClick(string nombreBoton)
    {
        // Asignar el nombre del representante
        GameManager.miRepresentante = nombreBoton;



        // Cambiar a la escena SELECCIONE_RECLAMO
        SceneManager.LoadScene("ELIGE_DIFICULTAD");
    }

    void Update()
    {
        // Puedes poner código de actualización aquí si es necesario
    }
}
