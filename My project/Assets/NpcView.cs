using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcView : MonoBehaviour
{
    public Animator anim;
    public SpriteRenderer spr;
    public Rigidbody2D rb2d;

    private void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb2d = GetComponentInParent<Rigidbody2D>();
    }

    private void Update()
    {
        if (rb2d.velocity.x<0)
        {
            spr.flipX = false;

        }
        else
        {
            spr.flipX = true;
        }
    }
}
