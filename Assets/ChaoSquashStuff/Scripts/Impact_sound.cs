using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impact_sound : MonoBehaviour
{
    public Material[] mats;
    public AudioClip[] snds;
    public AudioClip def;
    AudioSource aud;
    // Start is called before the first frame update
    void Start()
    {
        aud = GetComponent<AudioSource>();
        aud.spatialBlend = 1;
    }

    private void OnCollisionEnter(Collision collision)
    {
        MeshRenderer a;
        if (collision.collider.gameObject.TryGetComponent<MeshRenderer>(out a)) {
            if ((!collision.collider.gameObject.CompareTag("Destructible"))&&(!aud.isPlaying))
            {
                aud.volume = Mathf.Min(collision.impulse.magnitude * 2, 1);
                aud.pitch = Random.Range(0.8f, 1.2f);
                aud.clip = def;
                /*
                for (int i = 0; i < mats.Length; i++)
                {
                    if (a.material == mats[i])
                    {
                        aud.clip = snds[i];
                        break;
                    }
                }
                */
                aud.Play();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
