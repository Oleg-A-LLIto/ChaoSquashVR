using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class sound_vol : MonoBehaviour
{
    float vol = 0;
    public string thing;
    public AudioMixer mxr;
    public Slider sld;

    // Start is called before the first frame update
    void Start()
    {
        sld.value = (80+vol) / 80;
    }

    public void plussnd()
    {
        if (vol < 0)
        {
            vol += Time.deltaTime * 40;
            sld.value = (80 + vol) / 80;
            mxr.SetFloat(thing, vol);
        }
    }

    public void minussnd()
    {
        if (vol > -80)
        {
            vol -= Time.deltaTime * 40;
            sld.value = (80 + vol) / 80;
            mxr.SetFloat(thing, vol);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
