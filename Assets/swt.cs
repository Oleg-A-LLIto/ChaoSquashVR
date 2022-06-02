using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swt : MonoBehaviour
{
    public GameObject to;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void sw()
    {
        to.SetActive(true);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
