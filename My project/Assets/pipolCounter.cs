using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class pipolCounter : MonoBehaviour
{
    public Lider ld;
    public int pipolCount;
    public Collider2D[] gente;
    public Collider2D[] repres;
    public TextMeshProUGUI tmp;
  
   
    // Start is called before the first frame update
    void Start()
    {
        tmp=GetComponent<TextMeshProUGUI>();
        ld=GameObject.Find("Lider").GetComponent<Lider>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        gente = Physics2D.OverlapCircleAll(ld.transform.position, ld.influenceRadius, 1 << 8);
        repres = Physics2D.OverlapCircleAll(ld.transform.position, ld.influenceRadius, 1 << 9);
        pipolCount = gente.Length;
        tmp.text = "Hay " +(repres.Length+1+pipolCount) + " personas manifestandose";
    }
}
