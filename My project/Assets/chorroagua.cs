using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class chorroagua : MonoBehaviour

{
    public float fuerzaChorro;
    public ParticleSystem part;
    public List<ParticleCollisionEvent> collisionEvents;

    void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);

        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        int i = 0;

        while (i < numCollisionEvents)
        {
            if (rb && other.gameObject.layer == 8)
            {
                Vector3 pos = collisionEvents[i].intersection;
                Vector3 force = collisionEvents[i].velocity * fuerzaChorro;
                rb.AddForce(force);
               

            }
            i++;
        }
    }
}
