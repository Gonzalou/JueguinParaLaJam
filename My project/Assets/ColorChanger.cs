
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public multitudNPC myNPC;
    public Material material; // Asigna tu material con el shader a través del Inspector
    public SpriteRenderer spr;
    public Color[] coloresDePelo;
    public Color[] coloresDeRopa;
    public Color[] coloresDePiel;

    private void Awake()
    {
        material = Instantiate(material);       


    }
    void Start()
    {
        if (material != null)
        {
            spr.material = material;

            // Verifica si el material y la propiedad existen antes de intentar acceder a ellas
            if (material.HasProperty("_Pelo"))
            {

                // Accede y modifica la propiedad de color
                material.SetColor("_Pelo", coloresDePelo[Random.Range(0, coloresDePelo.Length)]); 
            }
            else
            {
                Debug.LogError("pelo no encontrado");
            }

            if (material.HasProperty("_Piel"))
            {

                
                // Accede y modifica la propiedad de color
                material.SetColor("_Piel", coloresDePiel[Random.Range(0, coloresDePiel.Length)]); 
            }
            else
            {
                Debug.LogError("piel no encontrado");
            }

            material.SetColor("_Ropa", coloresDeRopa[myNPC.representanteINDEX - 1]);
        }

    }

    private void Update()
    {
        if (myNPC.representanteINDEX == 0)
        {
            material.SetColor("_Ropa", Color.grey);
        }
    }
}
