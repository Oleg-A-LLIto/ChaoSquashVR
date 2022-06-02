using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnChaos : MonoBehaviour
{
    public float ChaosThreshold = 100;
    public float ChaosDelete = 0;
    GameObject mommy;
    public GameObject tospawn;
    public GameObject[] room_pool;
    bool alive = true;
    public bool endless = false;
    GameObject done;
    public bool dieatspawn = false;
    // Start is called before the first frame update
    void Start()
    {
        mommy = GameObject.FindGameObjectWithTag("GameController");
        if (dieatspawn)
        {
            alive = false;
            tospawn = room_pool[Random.Range(0, room_pool.Length - 1)];
            done = Instantiate(tospawn);
            done.transform.position = transform.position;
            done.transform.rotation = Quaternion.Euler(new Vector3(0, 90 * Random.Range(0, 3), 0));
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float chaos = (mommy.GetComponent<ChaosMeter>().initchaos + ((mommy.GetComponent<ChaosMeter>().chaos > 12.5) ? 15 : 0)) % 100;
        if (alive)
        {
            if (((chaos > ChaosThreshold) && ((ChaosDelete<ChaosThreshold) || chaos<ChaosDelete))) //ok
            {
                alive = false;
                if (endless)
                    tospawn = room_pool[Random.Range(0, room_pool.Length-1)];
                done = Instantiate(tospawn);
                done.transform.position = transform.position;
                if (endless)
                    done.transform.rotation = Quaternion.Euler(new Vector3(0, 90 * Random.Range(0, 3), 0));
            }
        }
        else
        {
            if (endless)
            {
                if ((chaos > ChaosDelete) && ((ChaosDelete > ChaosThreshold) || chaos < ChaosThreshold))
                {
                    alive = true;
                    StartCoroutine(DeathAnimation());
                }
            }
        }
        IEnumerator DeathAnimation()
        {
            yield return new WaitForSecondsRealtime(2);
            Destroy(done);
            //gameObject.SetActive(false);
            //yield return 0;
        }
    }
}
