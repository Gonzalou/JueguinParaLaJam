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

    [SerializeField] private Button abuelaButtonStats;
    [SerializeField] private Button academicoButtonStats;
    [SerializeField] private Button gremialistaButtonStats;

    [SerializeField] private Image AbuelaStats;
    [SerializeField] private Image AcademicoStats;
    [SerializeField] private Image GremialistaStats;

    // Representa el botón de estadísticas que está actualmente activo
    private Button activeStatsButton;

    void Start()
    {
        abuelaButton.onClick.AddListener(() => OnButtonClick("Abuela"));
        academicoButton.onClick.AddListener(() => OnButtonClick("Academico"));
        gremialistaButton.onClick.AddListener(() => OnButtonClick("Gremialista"));

        abuelaButtonStats.onClick.AddListener(() => OnStatsButtonClick(abuelaButtonStats, "AbuelaStats"));
        academicoButtonStats.onClick.AddListener(() => OnStatsButtonClick(academicoButtonStats, "AcademicoStats"));
        gremialistaButtonStats.onClick.AddListener(() => OnStatsButtonClick(gremialistaButtonStats, "GremialistaStats"));

        OcultarStats();
    }

    void OnButtonClick(string nombreBoton)
    {
        OcultarStats();

        switch (nombreBoton)
        {
            case "Abuela":
            case "Academico":
            case "Gremialista":
                SceneManager.LoadScene("ELIGE_DIFICULTAD");
                GameManager.miRepresentante = nombreBoton;
                ImprimirInfo();
                break;
        }
    }

    void OnStatsButtonClick(Button statsButton, string nombreStats)
    {
        if (activeStatsButton == statsButton)
        {
            // Si se presiona nuevamente el mismo botón de estadísticas, oculta la imagen
            OcultarStats();
            activeStatsButton = null;
        }
        else
        {
            // Muestra la imagen de estadísticas correspondiente al botón presionado
            OcultarStats();
            MostrarStats(nombreStats);
            activeStatsButton = statsButton;
        }
    }

    void OcultarStats()
    {
        AbuelaStats.gameObject.SetActive(false);
        AcademicoStats.gameObject.SetActive(false);
        GremialistaStats.gameObject.SetActive(false);
    }

    void MostrarStats(string nombreStats)
    {
        switch (nombreStats)
        {
            case "AbuelaStats":
                AbuelaStats.gameObject.SetActive(true);
                break;
            case "AcademicoStats":
                AcademicoStats.gameObject.SetActive(true);
                break;
            case "GremialistaStats":
                GremialistaStats.gameObject.SetActive(true);
                break;
        }
    }

    void ImprimirInfo()
    {
        Debug.Log("Dificultad: " + GameManager.dificultad);
        Debug.Log("Representante: " + GameManager.miRepresentante);
        Debug.Log("Reclamo " + GameManager.reclamo);
    }

    void Update()
    {
        // Puedes poner código de actualización aquí si es necesario
    }
}

