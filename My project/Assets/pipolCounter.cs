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
    public TextMeshPro tmp;
    // Start is called before the first frame update
    void Start()
    {
        ld=GameObject.Find("Lider").GetComponent<Lider>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        gente = Physics2D.OverlapCircleAll(transform.position, ld.influenceRadius, 1 << 8);
        pipolCount = gente.Length;
    }
}
