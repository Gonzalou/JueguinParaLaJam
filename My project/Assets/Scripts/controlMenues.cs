using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ControlMenus : MonoBehaviour
{
    [Header("Difficulty Buttons")]
    [SerializeField] private Button facilButton;
    [SerializeField] private Button medioButton;
    [SerializeField] private Button dificilButton;

    [Header("Static Variables")]
    public static string Dificultad;
    public static string MiRepresentante;
    public static string Reclamo;

    [Header("UI Panels")]
    public GameObject eligeDificultad;
    public GameObject eligeReclamo;
    public GameObject eligeRepresentante;

    [Header("Representative Buttons")]
    [SerializeField] private Button abuelaButton;
    [SerializeField] private Button academicoButton;
    [SerializeField] private Button gremialistaButton;

    [Header("Representative Stats Buttons")]
    [SerializeField] private Button abuelaStatsButton;
    [SerializeField] private Button academicoStatsButton;
    [SerializeField] private Button gremialistaStatsButton;

    [Header("Representative Stats Images")]
    [SerializeField] private Image StatsAbuela;
    [SerializeField] private Image StatsAcademico;
    [SerializeField] private Image StatsGremialista;

    [Header("Complaint Input")]
    [SerializeField] private TMP_InputField reclamoInputField;
    [SerializeField] private Button reclamaYaButton;
    [SerializeField] private Button volverButton;

    private void Start()
    {
        RepresentantePanel();
    }

    private void SetPanelVisibility(GameObject panel, bool isVisible)
    {
        if (panel != null)
            panel.SetActive(isVisible);
    }

    private void SetStatsVisibility(Image statsImage, bool isVisible)
    {
        if (statsImage != null)
            statsImage.enabled = isVisible;
    }

    private void OcultarStats()
    {
        SetStatsVisibility(StatsAbuela, false);
        SetStatsVisibility(StatsAcademico, false);
        SetStatsVisibility(StatsGremialista, false);
    }

  

    private void ShowStats(Image statsImage)
{
    OcultarStats();

    if (statsImage != null)
    {
        SetStatsVisibility(statsImage, true);
        Debug.Log("Botón presionado");
    }
    else
    {
        Debug.LogWarning("StatsImage is null.");
    }
}



    public void RepresentantePanel() => SetPanelVisibility(eligeRepresentante, true);

    public void ReclamoPanel() => SetPanelVisibility(eligeReclamo, true);

    public void DificultadPanel()
    {
        SetPanelVisibility(eligeDificultad, true);
        OcultarStats();
        Debug.Log("Botón presionado");
    }

    public void AbuelaStats() => ShowStats(StatsAbuela);

    public void GremialistaStats() => ShowStats(StatsGremialista);

    public void AcademicoStats() => ShowStats(StatsAcademico);

    public void IniciarJuego() => SceneManager.LoadScene("Level1");
}
