using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stoppable : MonoBehaviour
{
    Vector3 speed = Vector3.zero;
    Rigidbody rb;
    // Start is called before the first frame update

    public void pause()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
        if (rb.velocity.magnitude > 0.1f)
        {
            speed = rb.velocity;
            rb.isKinematic = true;
        }
    }

    public void unpause()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
        rb.isKinematic = false;
        if (speed.magnitude > 0.1f)
        {
            rb.velocity = speed;
        }
    }


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
