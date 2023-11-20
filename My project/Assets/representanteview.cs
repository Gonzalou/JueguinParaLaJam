using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class representanteview : MonoBehaviour
   
{
    public SpriteRenderer spr;
    public Animator anim;
    public Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb2d = gameObject.GetComponentInParent<Rigidbody2D>();
        anim.SetBool("walk", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (rb2d.velocity.x > 0)
        {
            spr.flipX = true;
        }
        else
        {
            spr.flipX = false;
        }
        if (rb2d.velocity.magnitude >0)
        {
            anim.SetBool("walk", false);
        }
        else
        {
            anim.SetBool("walk", true);
        }
    }
}
