using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem.Sample
{
    public class DestroyedByChaos : MonoBehaviour
    {
        GameObject mommy;
        AudioSource[] a;
        public float ChaosThreshold = 100;
        bool alive = true;
        public bool endless_mode = false;
        public float ChaosDelete = 0;
        public bool dieatspawn = false;

        void Start()
        {
            mommy = GameObject.FindGameObjectWithTag("GameController");
            a = GetComponentsInChildren<AudioSource>(false);
            if (dieatspawn)
            {
                alive = false;
                StartCoroutine(DeathAnimation());
                a[1].Play();
            }
        }

        // Update is called once per frame
        void LateUpdate()
        {
            float chaos = (mommy.GetComponent<ChaosMeter>().initchaos + ((mommy.GetComponent<ChaosMeter>().chaos > 12.5) ? 15 : 0)) % 100;
            if (alive)
            {
                if ((chaos > ChaosThreshold) && ((ChaosDelete < ChaosThreshold) || chaos < ChaosDelete))
                {
                    alive = false;
                    StartCoroutine(DeathAnimation());
                    a[1].Play();
                    if(mommy.GetComponent<ChaosMeter>().chaos>10)
                        mommy.GetComponent<Initiate_gaem>().need_upgrade();
                }
            }
            else
            {
                if (endless_mode)
                {
                    if ((chaos > ChaosDelete) && ((ChaosDelete > ChaosThreshold) || chaos < ChaosThreshold))
                    {
                        alive = true;
                        StartCoroutine(RebirthAnimation());
                        a[1].Play();
                        mommy.GetComponent<Initiate_gaem>().getballback();
                    }
                }
            }
        }

        IEnumerator DeathAnimation()
        {
            while (transform.position.y > -2)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - Time.deltaTime, transform.position.z);
                yield return 0;
            }
            //gameObject.SetActive(false);
            //yield return 0;
        }

        IEnumerator RebirthAnimation()
        {
            while (transform.position.y < 2)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime, transform.position.z);
                yield return 0;
            }
            //gameObject.SetActive(false);
            //yield return 0;
        }
    }
}
