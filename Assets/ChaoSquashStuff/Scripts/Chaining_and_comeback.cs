using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaining_and_comeback : MonoBehaviour
{
    public bool counting = false;
    public bool hitMutex = false;
    public bool explosive = false;
    public bool exploded = true;
    AudioSource aud;
    public AudioClip ExplSound;
    public int num = 0;
    public int chain_length = 6;
    float wait = 0.5f;
    bool hit_home = false;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        aud = GetComponent<AudioSource>();
    }

    void LaunchY(Vector3 from, Vector3 to, float angle)
    {
        //Debug.Log("Launching: ");
        //Debug.Log(from);
        //.Log(to);
        //Debug.Log(angle);
        // think of it as top-down view of vectors: 
        //   we don't care about the y-component(height) of the initial and target position.
        Vector3 projectileXZPos = new Vector3(from.x, 0, from.z);
        //Debug.Log(projectileXZPos);
        Vector3 targetXZPos = new Vector3(to.x, 0, to.z);
        //Debug.Log(targetXZPos);

        // rotate the object to face the target
        transform.LookAt(targetXZPos);

        // shorthands for the formula
        float R = Vector3.Distance(projectileXZPos, targetXZPos);
        //Debug.Log(R);
        float G = Physics.gravity.y;
        //Debug.Log(G);
        float tanAlpha = Mathf.Tan(angle * Mathf.Deg2Rad);
        //Debug.Log(tanAlpha);
        float H = to.y - from.y;
        //Debug.Log(H);

        // calculate the local space components of the velocity 
        // required to land the projectile on the target object 
        //float s = Mathf.Sign(G * R * R / (2.0f * (H - R * tanAlpha)));
        //float Vz = s * Mathf.Sqrt(Mathf.Abs(G * R * R / (2.0f * (H - R * tanAlpha))));
        float Vz = Mathf.Sqrt(Mathf.Abs(G * R * R / (2.0f * (H - R * tanAlpha))));
        //Debug.Log(Vz);
        float Vy = tanAlpha * Vz;
        //Debug.Log(Vy);

        // create the velocity vector in local space and get it in global space
        Vector3 localVelocity = new Vector3(0f, Vy, Vz);
        //Debug.Log(localVelocity);
        Vector3 globalVelocity = transform.TransformDirection(localVelocity);
        if (angle == 0)
            globalVelocity /= 1.15f;
        //Debug.Log(globalVelocity);

        // launch the object by setting its initial velocity and flipping its state
        GetComponent<Rigidbody>().velocity = globalVelocity;
    }

    IEnumerator LaunchX(Vector3 from, Vector3 to, float angle)
    {
        hitMutex = true;
        for (float i = 0; i < 0.08f; i += Time.deltaTime)
        {
            yield return 0;
        }
        LaunchY(from, to, angle);
        hitMutex = false;
    }

    void Launch(Vector3 from, Vector3 to, float angle)
    {
        StartCoroutine(LaunchX(from, to, angle));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((wait < 0.5f) || (hitMutex))
        {
            return;
        }
        //Debug.Log("bonk");
        if (counting && (!collision.collider.gameObject.CompareTag("Hand")) && (!collision.collider.gameObject.CompareTag("Barrier")))
        {
            if (!collision.collider.gameObject.CompareTag("Racket"))
            {
                //Debug.Log("Just hit the wall or a thing");
                if (collision.collider.gameObject.CompareTag("Destructible"))
                {
                    if (!exploded)
                    {
                        explode();
                        exploded = true;
                    }
                }
                num += 1;
                //Debug.Log("Collision number "+num);
                if (num < chain_length)
                {
                    GameObject[] aim = GameObject.FindGameObjectsWithTag("Destructible");
                    GameObject fire = aim[0];
                    float maxhealth = 0;
                    for (int i = 0; i < aim.Length; i++)
                    {
                        float aimdist = (aim[i].transform.position - transform.position).magnitude;
                        if (aimdist > 2 && aimdist < 5)
                        {
                            if (aim[i].GetComponent<destructible>().health > 80)
                            {
                                fire = aim[i];
                                break;
                            }
                            else
                            {
                                if (aim[i].GetComponent<destructible>().health > maxhealth)
                                {
                                    maxhealth = aim[i].GetComponent<destructible>().health;
                                    fire = aim[i];
                                }
                            }
                        }
                        //Debug.Log("collided with " + collision.collider.name + " now aiming at " + fire.name + " collision " + num + "/" + chain_length + " with health of " + fire.GetComponent<destructible>().health);
                    }
                    hit_home = false;
                    /*
                    Vector3 force_guess = fire.transform.position - transform.position;
                    GetComponent<Rigidbody>().velocity = Vector3.zero;
                    GetComponent<Rigidbody>().AddForce(force_guess / Time.deltaTime * 96);
                    */
                    if ((collision.collider.gameObject.CompareTag("Destructible")))
                        Launch(transform.position, fire.transform.position, 10);
                    else
                        LaunchY(transform.position, fire.transform.position, 10);
                }
                else
                {
                    if(player == null)
                    {
                        DeathBehaviour();
                        return;
                    }
                    float angle = Vector2.Angle(new Vector2(player.transform.position.x, player.transform.position.z), new Vector2(1, 0)) + Random.Range(-15, 15);
                    float dist = Random.Range(0, 1f);
                    /*
                    GetComponent<Rigidbody>().velocity = Vector3.zero;
                    Vector3 force_guess = new Vector3(Mathf.Sin(angle) * dist, 2, Mathf.Cos(angle) * dist) - transform.position;
                    GetComponent<Rigidbody>().AddForce(force_guess / Time.deltaTime * 48);
                    Debug.Log("cumming back home");
                    */
                    if ((Mathf.Abs(transform.position.x) > 2.5f) || (Mathf.Abs(transform.position.z) > 2.5f))
                    {
                        if ((collision.collider.gameObject.CompareTag("Destructible")))
                            Launch(transform.position, new Vector3(Mathf.Sin(angle) * dist, 1.5f, Mathf.Cos(angle) * dist), 50);
                        else
                            LaunchY(transform.position, new Vector3(Mathf.Sin(angle) * dist, 1.5f, Mathf.Cos(angle) * dist), 50);
                    }
                    if (num >= chain_length)
                    {
                        if ((Mathf.Abs(transform.position.x) < 2.5f) && (Mathf.Abs(transform.position.z) < 2.5f))
                        {
                            if (hit_home)
                            {
                                //Debug.Log("Ok I gave yo a chance I'm coming back");
                                GetComponent<LockToPoint>().works = true;
                            }
                            else
                            {
                                //Debug.Log("Ok you can try and stop me");
                                hit_home = true;
                            }
                        }
                    }
                }
            }
            else
            {
                num = 0;
                hit_home = false;
                if (explosive)
                    exploded = false;
                //Debug.Log("collided with a racket, num is now 0");
            }
        }
    }

    private void DeathBehaviour()
    {
        GameObject.Destroy(gameObject);
    }

    void explode()
    {
        GetComponentInChildren<ParticleSystem>().Play();
        Collider[] victims = Physics.OverlapSphere(transform.position, 0.3f);
        aud.volume = 2;
        aud.pitch = Random.Range(0.8f, 1.2f);
        aud.clip = ExplSound;
        aud.Play();
        foreach (Collider thing in victims)
        {
            Rigidbody a;
            if (thing.gameObject.TryGetComponent<Rigidbody>(out a))
            {
                a.AddExplosionForce(30000, transform.position, 0.3f);
                destructible b;
                if (thing.gameObject.TryGetComponent<destructible>(out b))
                {
                    b.health -= 100 / (transform.position - a.position).magnitude / a.mass * 7;
                }
            }
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (wait < 0.5f)
        {
            wait += Time.deltaTime;
        }
    }
}
