using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destructible : MonoBehaviour
{
    GameObject mommy;
    public float health = 100;

    void Start()
    {
        mommy =  GameObject.FindGameObjectWithTag("GameController");
        GetComponent<AudioSource>().spatialBlend = 1;
        GetComponent<AudioSource>().priority = 255;
    }

    private void OnJointBreak(float breakForce)
    {
        mommy.GetComponent<ChaosMeter>().chaos += breakForce / 10000;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Bol"))
        {
            mommy.GetComponent<ChaosMeter>().chaos += collision.impulse.magnitude * GetComponent<Rigidbody>().mass / 10000000;         
        }
        else
        {
            //Debug.Log("hit by not a ball");
            if (mommy==null)
            {
                mommy = GameObject.FindGameObjectWithTag("GameController");
            }
            mommy.GetComponent<ChaosMeter>().chaos += collision.impulse.magnitude * GetComponent<Rigidbody>().mass / 1000000000;
        }
        if (!GetComponent<AudioSource>().isPlaying)
        {
            float vol = collision.impulse.magnitude;
            if (vol > 0.1f)
            {
                //Debug.Log("I MADE A SOUND "+vol);
                GetComponent<AudioSource>().pitch = Random.Range(0.8f, 1);
                GetComponent<AudioSource>().volume = Mathf.Min(1, vol);
                GetComponent<AudioSource>().Play();
            }
        }
        health -= collision.impulse.magnitude / GetComponent<Rigidbody>().mass * 3;
        if (health <= 0)
        {
            //Debug.Log("Hi I'm " + gameObject.name + " and I'm commiting dead with health of " + health);
            StartCoroutine(Die());
        }
        //if (collision.impulse.magnitude > 10)
        //{
        //    gameObject.SetActive(false);
        //}
    }

    IEnumerator Die()
    {
            MeshRenderer output;
            if (TryGetComponent<MeshRenderer>(out output))
            {
                for (float i = 0; i < 1; i += Time.deltaTime)
                {
                    output.material.SetFloat("_Amount", i);
                    yield return 0;
                }
            }
            else
            {
                MeshRenderer[] allout = GetComponentsInChildren<MeshRenderer>();
                for (float i = 0; i < 1; i += Time.deltaTime)
                {
                    for(int j = 0; j<allout.Length; j++)
                    {
                        allout[j].material.SetFloat("_Amount", i);
                        yield return 0;
                    }
                }
            }
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        MeshRenderer output;
        if (TryGetComponent<MeshRenderer>(out output))
        {
            for (float i = 0; i < 1; i += Time.deltaTime)
            {
                output.material.SetFloat("Amount", (100 - health) / 100);
            }
        }
        else
        {
            MeshRenderer[] allout = GetComponentsInChildren<MeshRenderer>();
            for (float i = 0; i < 1; i += Time.deltaTime)
            {
                for (int j = 0; j < allout.Length; j++)
                {
                    allout[j].material.SetFloat("Amount", (100 - health) / 100);
                    
                }
            }
        }
        */
    }
}
