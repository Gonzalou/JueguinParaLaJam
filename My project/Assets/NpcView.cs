using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcView : MonoBehaviour
{
    public Animator anim;
    public SpriteRenderer spr;
    public Rigidbody2D rb2d;
    public Dano dano;
    public float animSpeed;
    private void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb2d = GetComponentInParent<Rigidbody2D>();
        dano = GetComponentInParent<Dano>();
    }

    private void Update()
    {
        if (rb2d.velocity.magnitude > 0.5)
        {
            anim.speed = rb2d.velocity.magnitude*animSpeed;
            anim.SetBool("Walk", true);          
        }
        else
        {
            anim.speed = 1;
            anim.SetBool("Walk", false);
        }

        if (dano.life <= 0)
        {
            anim.SetBool("Dead", true);
        }

        if (rb2d.velocity.x < 0)
        {
            spr.flipX = false;

        }
        else
        {
            spr.flipX = true;
        }
    }
}
