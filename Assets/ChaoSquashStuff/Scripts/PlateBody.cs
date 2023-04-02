using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateBody : MonoBehaviour
{
    [SerializeField] BreakableChoice Choice;
    [SerializeField] float forceThreshold;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.impulse.magnitude > forceThreshold)
        {
            Choice.Choose(gameObject, collision.impulse.magnitude);
        }
    }
}
