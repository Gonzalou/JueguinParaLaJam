using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        SeleccionRepresentante,
        SeleccionDificultad,
        IntroducirReclamo
    }

    private GameState gameState;

    private Button activeStatsButton;

    public static string dificultad;
    public static string miRepresentante;
    public static string reclamo;

    [SerializeField] private Button abuelaButton;
    [SerializeField] private Button academicoButton;
    [SerializeField] private Button gremialistaButton;

    [SerializeField] private Button abuelaStatsButton;
    [SerializeField] private Button academicoStatsButton;
    [SerializeField] private Button gremialistaStatsButton;

    [SerializeField] private Image AbuelaStats;
    [SerializeField] private Image AcademicoStats;
    [SerializeField] private Image GremialistaStats;

    [SerializeField] private Button facilButton;
    [SerializeField] private Button medioButton;
    [SerializeField] private Button dificilButton;

    [SerializeField] private TMP_InputField reclamoInputField;
    [SerializeField] private Button reclamaYaButton;

    void Start()
    {
        SetGameState(GameState.SeleccionRepresentante);
        UpdateButtonListeners();
        OcultarStats();
        ConfigureReclamoButton();
    }

    void SetGameState(GameState newState)
    {
        gameState = newState;
        UpdateButtonListeners();
    }

    void UpdateButtonListeners()
    {
        abuelaButton.onClick.RemoveAllListeners();
        academicoButton.onClick.RemoveAllListeners();
        gremialistaButton.onClick.RemoveAllListeners();
        abuelaStatsButton.onClick.RemoveAllListeners();
        academicoStatsButton.onClick.RemoveAllListeners();
        gremialistaStatsButton.onClick.RemoveAllListeners();
        facilButton.onClick.RemoveAllListeners();
        medioButton.onClick.RemoveAllListeners();
        dificilButton.onClick.RemoveAllListeners();
        reclamaYaButton.onClick.RemoveAllListeners();

        switch (gameState)
        {
            case GameState.SeleccionRepresentante:
                abuelaButton.onClick.AddListener(() => OnRepresentanteButtonClick("Abuela"));
                academicoButton.onClick.AddListener(() => OnRepresentanteButtonClick("Academico"));
                gremialistaButton.onClick.AddListener(() => OnRepresentanteButtonClick("Gremialista"));
                abuelaStatsButton.onClick.AddListener(() => OnStatsButtonClick(abuelaStatsButton, "AbuelaStats"));
                academicoStatsButton.onClick.AddListener(() => OnStatsButtonClick(academicoStatsButton, "AcademicoStats"));
                gremialistaStatsButton.onClick.AddListener(() => OnStatsButtonClick(gremialistaStatsButton, "GremialistaStats"));
                break;
            case GameState.SeleccionDificultad:
                facilButton.onClick.AddListener(() => OnDificultadButtonClick("facil"));
                medioButton.onClick.AddListener(() => OnDificultadButtonClick("medio"));
                dificilButton.onClick.AddListener(() => OnDificultadButtonClick("dificil"));
                break;
            case GameState.IntroducirReclamo:
                ConfigureReclamoButton();
                break;
        }
    }

    void OnRepresentanteButtonClick(string representante)
    {
        OcultarStats();
        miRepresentante = representante;
        ImprimirInfo();
        SetGameState(GameState.SeleccionDificultad);
    }

    void OnStatsButtonClick(Button statsButton, string nombreStats)
    {
        if (activeStatsButton == statsButton)
        {
            OcultarStats();
            activeStatsButton = null;
        }
        else
        {
            OcultarStats();
            MostrarStats(nombreStats);
            activeStatsButton = statsButton;
        }
    }

    void OnDificultadButtonClick(string selectedDificultad)
    {
        dificultad = selectedDificultad;
        SetGameState(GameState.IntroducirReclamo);
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
        Debug.Log("Dificultad: " + dificultad);
        Debug.Log("Representante: " + miRepresentante);
        Debug.Log("Reclamo " + reclamo);
    }

    void ConfigureReclamoButton()
    {
        reclamaYaButton.onClick.AddListener(ReclamaYa);
    }

    void ReclamaYa()
    {
        string reclamo = reclamoInputField.text;

        if (reclamo.Length <= 30)
        {
            GameManager.reclamo = reclamo;
            SceneManager.LoadScene("Level1");
            ImprimirInfo();
        }
        else
        {
            Debug.LogError("El reclamo debe tener menos de 30 caracteres.");
        }
    }
}
