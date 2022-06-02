using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothlyFollow : MonoBehaviour
{
    Rigidbody body;
    public Transform snapTo;
    public float fastboi;
    public bool following;
    public GameObject head;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - head.transform.position) * Quaternion.Euler(-85, 0, 0);
        if ((body.position - snapTo.position).magnitude > 0.6f)
        {
            following = true;
        }

        if ((body.position - snapTo.position).magnitude < 0.1f)
        {
            following = false;
        }

        if (following)
        {
            body.velocity = Vector3.Lerp(body.velocity, Vector3.zero, Time.fixedDeltaTime * fastboi * 4);
            transform.position = Vector3.Lerp(transform.position, snapTo.position, Time.fixedDeltaTime * fastboi * 3);
            //transform.rotation = Quaternion.Slerp(transform.rotation, snapTo.rotation, Time.fixedDeltaTime * fastboi * 2);
        }
    }
}
