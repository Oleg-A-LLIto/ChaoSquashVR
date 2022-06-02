using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ChaosMeter : MonoBehaviour
{
    public float chaos = 0;
    float chaosprev = 0;
    public float initchaos = 0;
    public AudioSource chaotic_music;
    public AudioSource chill_music;
    public GameObject rack;
    public Slider ChaosSlider;
    public Slider InitChaosSlider;
    public bool pause;
    private float mod = 1;
    private int followed;
    public bool endless;
    private float[] bois;
    private int ctr = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Initial_resistance());
        followed = Mathf.RoundToInt(1 / Time.fixedDeltaTime);
        bois = new float[followed];
    }

    IEnumerator Initial_resistance()
    {
        for (float i = 0; i < 0.5f; i += Time.deltaTime)
        {
            chaos = 0;
            yield return 0;
        }
    }

    public void slow_down()
    {
        mod *= 0.9f;
    }

    float refresh_sum()
    {
        bois[ctr] = chaos - chaosprev;
        chaosprev = chaos;
        ctr += 1;
        if(ctr == followed)
        {
            ctr = 0;
        }
        float sum = 0;
        for (int i = 0; i < followed; i++)
        {
            sum += bois[i];
        }
        return sum;
    }

    // Update is called once per frame
    void Update()
    {
        if (!pause)
        {
            //Debug.Log(chaosprev);
            //Debug.Log(chaos);
            float sum = refresh_sum();
            if (sum > 5)
            {
                chaos -= sum - 5;
                Debug.Log("Destroyed " + (sum - 5) + " Chaos");
            }
            /*
            Debug.Log(chaos - chaosprev);
            if (chaos > (chaosprev + Time.deltaTime * 10))
            {
                Debug.Log("Limited the extra " + (chaos - (chaosprev + Time.deltaTime * 2))+" chaos");
                chaos = chaosprev + Time.deltaTime * 10;
            }
            */
            if (!endless)
            {
                chaos -= Time.deltaTime * chaos / 250 * mod;
            }
            else
            {
                float reduce = Time.deltaTime * chaos / 350 * mod;
                if (Mathf.RoundToInt(chaos - reduce)/ 25 == Mathf.RoundToInt(chaos - reduce) % 25)
                {
                    chaos -= reduce;
                }
            }
            
            if ((chaos - initchaos) < 10)
            {
                chaotic_music.volume = Mathf.Max((chaos - initchaos) / 10 * 0.4f,0);
                chill_music.volume = 0.4f - chaotic_music.volume;
            }
            else
            {
                chill_music.volume = 0;
                chaotic_music.volume = 0.4f;
            }
            if (initchaos < chaos - 15)
            {
                initchaos = chaos - 15;
            }
            if (!chill_music.isPlaying)
            {
                chill_music.Play();
            }
            if (!chaotic_music.isPlaying)
            {
                chaotic_music.Play();
            }
        }
        
    }

    private void LateUpdate()
    {
        ChaosSlider.value = (chaos % 100) / 100;
        if((Mathf.RoundToInt(chaos)/100)== (Mathf.RoundToInt(initchaos) / 100))
            InitChaosSlider.value = (initchaos % 100) / 100;
        else
            InitChaosSlider.value = 0;
    }
}
