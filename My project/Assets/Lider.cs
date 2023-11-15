using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Lider : MonoBehaviour
{
    public float liderSpeed;
    public float defaultLiderSpeed;
    public float maxSpeed;

    public float defaultInfluenceRadious;
    public float influenceRadius;
    public float MaxInfluence;

    public TouchPosition tchP;
    public MousePosition mouse;
    public HudRepresentantes Hre;
    public Transform[] reprePositions;
    public bool llegue;
    public SpriteRenderer spr;
    public ParticleSystem visualInfluence;
    public Rigidbody2D rb2d;
    private bool juegoEnMobile;

    private VirtualDPad DPad;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        tchP = GetComponent<TouchPosition>();
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            juegoEnMobile = true;

            // Puedes realizar acciones específicas para dispositivos móviles aquí
        }
        else
        {
            juegoEnMobile = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        DPad = GameObject.Find("DPad").GetComponent<VirtualDPad>();
        llegue = false;
        defaultLiderSpeed = liderSpeed;
        defaultInfluenceRadious = influenceRadius;
        if (!juegoEnMobile)
        {
            DPad.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {



        if (defaultInfluenceRadious != influenceRadius)
        {

            SetRadioEmision(influenceRadius);

        }

        if (influenceRadius >= MaxInfluence) { influenceRadius = MaxInfluence; }


        if (!llegue)
        {

            if (!juegoEnMobile)
            {
                if (Input.GetAxis("Horizontal") < 0)
                {
                    spr.flipX = true;
                }
                else
                {
                    spr.flipX = false;
                }
                /*transform.position += (Vector3.right * Input.GetAxis("Horizontal")) * liderSpeed * Time.deltaTime;

                transform.position += (Vector3.up * Input.GetAxis("Vertical")) * liderSpeed * Time.deltaTime;*/

                rb2d.velocity += new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized * liderSpeed * Time.deltaTime;
            }
            else
            {
                
                rb2d.velocity += DPad.dir * liderSpeed * Time.deltaTime;

            }




        }


    }

    void SetRadioEmision(float nuevoRadio)
    { // Obtén el módulo Shape del sistema de partículas
        ParticleSystem.ShapeModule shapeModule = visualInfluence.shape;

        // Establece el nuevo radio en el módulo Shape
        shapeModule.radius = influenceRadius;

        // Asigna el nuevo radio a la variable nuevoRadio para futuras referencias
        nuevoRadio = influenceRadius;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 13)
        {
            llegue = true;
        }
    }
}
