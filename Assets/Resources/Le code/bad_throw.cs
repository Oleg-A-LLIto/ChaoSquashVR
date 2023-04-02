using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;
using Autohand;

public class bad_throw : MonoBehaviour
{
    bool defused = false;
    public Material defusedmat;
    public Material undefusedmat;
    GameObject[] bars;
    // Start is called before the first frame update


    void Start()
    {
        bars = GameObject.FindGameObjectsWithTag("Barrier");
    }
    protected virtual void OnDetachedFromHand(Hand hand)
    {
        defused = false;
        GetComponent<Chaining_and_comeback>().counting = false;
        unignore_bars();
    }


    void ignore_bars()
    {
        for (int i = 0; i < bars.Length; i++)
        {
            Physics.IgnoreCollision(GetComponentInChildren<Collider>(), bars[i].GetComponent<Collider>(), true);
        }
    }

    void unignore_bars()
    {
        for (int i = 0; i < bars.Length; i++)
        {
            Physics.IgnoreCollision(GetComponentInChildren<Collider>(), bars[i].GetComponent<Collider>(), false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!defused && !collision.collider.gameObject.CompareTag("Hand"))
        {
            if (!collision.collider.gameObject.CompareTag("Racket"))
            {
                GetComponent<LockToPoint>().works = true;
                //Debug.Log(collision.collider.gameObject.name);
            }
            else
            {
                defused = true;
                ignore_bars();
                GetComponent<Chaining_and_comeback>().counting = true;
                GetComponent<Chaining_and_comeback>().num = 0;
                Debug.Log("Touched the racket, starting the countdown.");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}