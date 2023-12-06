using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class Skills : MonoBehaviour
{
    public GameObject Desaparecido;
    public GameObject marcaGay;
    public GameObject explosionGremio;
    public int cantidadDeDesaparecidos;
    public int randoDeAparicion;
    public int buttonNum;
    public Button[] buttons;
    public List<Representante> repres = new List<Representante>();

    public int rangoDeStun;
    public int TiempoDeStun;

    public ParticleSystem panfletosParticle;
    public int fuerzaDeEmpuje;


    public int myRepresentantIndex;
    // Start is called before the first frame update
    void Start()
    {

        foreach (var item in GameObject.FindObjectsOfType<Representante>())
        {
            if (!repres.Contains(item))
            {
                repres.Add(item);
            }
        }
        for (int i = 0; i < buttons.Length - 1; i++)
        {
            buttons[i].onClick.AddListener(() => ActiveMySkill(i + 1));
        }
    }

    // Update is called once per frame
    void Update()
    {


        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
        {

            if (Input.GetKeyDown(KeyCode.Alpha1) && buttons[0].interactable) //ABUELAS 1
            {
                foreach (var item in repres)
                {
                    if (item.myIndex == 1 && item.habilidadOn)
                    {

                        ActiveMySkill(item.myIndex);
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) && buttons[1].interactable)//GREMIALISTAS 2
            {
                foreach (var item in repres)
                {
                    if (item.myIndex == 2 && item.habilidadOn)
                    {

                        ActiveMySkill(item.myIndex);
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha3) && buttons[2].interactable)//LGBT 3
            {
                foreach (var item in repres)
                {
                    if (item.myIndex == 3 && item.habilidadOn)
                    {

                        ActiveMySkill(item.myIndex);
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha4) && buttons[3].interactable)//ACADEMICOS 4
            {
                foreach (var item in repres)
                {
                    if (item.myIndex == 4 && item.habilidadOn)
                    {

                        ActiveMySkill(item.myIndex);
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha5) && buttons[4].interactable)//JOVENES 5
            {
                foreach (var item in repres)
                {
                    if (item.myIndex == 5 && item.habilidadOn)
                    {

                        ActiveMySkill(item.myIndex);
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha6) && buttons[5].interactable)//ZURDOS 6
            {
                foreach (var item in repres)
                {
                    if (item.myIndex == 6 && item.habilidadOn)
                    {

                        ActiveMySkill(item.myIndex);
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha7) && buttons[6].interactable) //FEMINISTAS 7
            {
                foreach (var item in repres)
                {
                    if (item.myIndex == 7 && item.habilidadOn)
                    {

                        ActiveMySkill(item.myIndex);
                    }
                }
            }
        }
    }


    public void ActiveMySkill(int index)
    {

        switch (index)
        {
            case 1: ABUELA(); break;
            case 2: GREMIALISTA(); break;
            case 3: LGBT(); break;
            case 4: ACADEMICOS(); break;
            case 5: JOVENES(); break;
            case 6: ZURDOS(); break;
            case 7: FEMINISTAS(); break;
        }
    }


    public void ABUELA()
    {
        int r = randoDeAparicion;
        for (int i = 0; i < cantidadDeDesaparecidos; i++)
        {
            Instantiate(Desaparecido, transform.position + Vector3.right * Random.Range(-r, r) + Vector3.up * Random.Range(-r, r), transform.rotation);

        }
    }
    public void GREMIALISTA()
    {
        foreach (var item in repres)
        {
            if (item.myIndex == 2)
            {
                GameObject grafico = Instantiate(explosionGremio, transform.position, transform.rotation);
                choquelogica ch = grafico.GetComponent<choquelogica>();
                ch.maxRadius = rangoDeStun;
                Collider2D[] enemigos = Physics2D.OverlapCircleAll(item.transform.position, rangoDeStun, 1 << 12);
                foreach (var enemy in enemigos)
                {
                    enemy.GetComponent<Enemigo>().tiempoStuneado = TiempoDeStun;
                }
            }
        }
    }
    public void LGBT()
    {
        Debug.Log("pum rayo gay");

        foreach (var item in repres)
        {
            if (item.myIndex == 3)
            {
                Collider2D[] enemigos = Physics2D.OverlapCircleAll(item.transform.position, rangoDeStun, 1 << 12);
                if (enemigos.Any())
                {
                    Enemigo e = enemigos[0].GetComponent<Enemigo>();
                    e.gameObject.layer = 22;
                    e.seHizoGay = true;
                    Instantiate(marcaGay, e.transform); //en el lgbt cambio el prefab desaparecido por el de la bandera

                }
            }
        }
    }
    public void ACADEMICOS()
    {
        panfletosParticle.Play();
    }
    public void JOVENES()
    {

    }
    public void ZURDOS()
    {

    }
    public void FEMINISTAS()
    {

    }


}
