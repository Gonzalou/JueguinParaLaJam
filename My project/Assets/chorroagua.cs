using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class chorroagua : MonoBehaviour
    
{
    private Rigidbody2D rb2d;
    public float fuerzaChorro;
    // Start is called before the first frame update
    void Start()
    {
       rb2d = GetComponent<Rigidbody2D>();
   
    }
   

    // Update is called once per frame
    void Update()
    {
        
    }
   
    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.CompareTag("awita"))
        {
            rb2d.AddForce((transform.position - other.transform.position).normalized * fuerzaChorro);
            Debug.Log("choco");

        }
        Debug.Log(other.gameObject.tag);
    }
}
