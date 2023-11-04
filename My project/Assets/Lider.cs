using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Lider : MonoBehaviour
{
    public float liderSpeed;
    public float defaultLiderSpeed;
    public float influenceRadius;
    public MousePosition mouse;
    public HudRepresentantes Hre;
    public Transform[] reprePositions;

    public SpriteRenderer spr;
    // Start is called before the first frame update
    void Start()
    {   
        defaultLiderSpeed = liderSpeed;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetAxis("Horizontal") < 0)
        {
            spr.flipX=true;
        }
        else
        {
            spr.flipX = false;
        }
            transform.position += (Vector3.right * Input.GetAxis("Horizontal")) * liderSpeed * Time.deltaTime;   
              
            transform.position += (Vector3.up * Input.GetAxis("Vertical")) * liderSpeed * Time.deltaTime;
        
    }

}
