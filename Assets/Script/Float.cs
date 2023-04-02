using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Float : MonoBehaviour
{
    [SerializeField] private float period;
    [SerializeField] private Vector3 Amplitude;
    private Vector3 initialPosition;
    private float t;

    void Start()
    {
        initialPosition = transform.position;
        t = 0;
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime / period * 360;
        if (t > 360)
            t = t - 360;
        transform.position = initialPosition + Amplitude * Mathf.Sin(t) * 2;
    }
}
