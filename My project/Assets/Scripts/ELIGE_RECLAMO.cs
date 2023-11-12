using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class IntroducirReclamo : MonoBehaviour
{

    [SerializeField] private TMP_InputField reclamoInputField;
    [SerializeField] private Button reclamaYaButton;

    // Start is called before the first frame update
    void Start()
    {
        // Asignar función al botón
        reclamaYaButton.onClick.AddListener(ReclamaYa);
    }

    // Función que se ejecuta al hacer clic en el botón
    void ReclamaYa()
    {
        // Obtener el texto introducido en el campo de reclamo
        string reclamo = reclamoInputField.text;

        // Verificar que el reclamo tenga menos de 30 caracteres
        if (reclamo.Length <= 30)
        {
            // Asignar el reclamo a una variable estática en GameManager
            GameManager.reclamo = reclamo;

            // Cargar la escena Level1
            SceneManager.LoadScene("Level1");

            // Imprime la dificultad elegida
            Debug.Log("Dificultad: " + GameManager.dificultad);
            Debug.Log("Representante: " + GameManager.miRepresentante);
            Debug.Log("Reclamo " + GameManager.reclamo);

        }
        else
        {
            // Si el reclamo tiene más de 30 caracteres, puedes mostrar un mensaje de error o realizar alguna acción adicional
            Debug.LogError("El reclamo debe tener menos de 30 caracteres.");
        }
    }
}
