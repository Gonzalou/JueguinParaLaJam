using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudRepresentantes : MonoBehaviour
{
    public bool HudOn;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        HudOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("InterfazOn", HudOn) ;
    }
}
