using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Initiate_gaem : MonoBehaviour
{
    public GameObject ball_obj;
    public int chain_length = 6;
    public List<GameObject> BALLS;
    //public GameObject ball_spawner;
    public GameObject UI_board;
    public GameObject Upgrades;
    public GameObject Menu;
    ChaosMeter chaosmeter;
    public GameObject[] ball_spawner;
    int numofballs = 0;
    GameObject[] destructibles;
    GameObject[] balls;
    Vector3[] dest_speeds;
    Vector3[] bol_speeds;
    public AudioClip pause_snd;
    public bool explosive = false;
    public int righhanded;
    public GameObject righthandedtablet;
    public GameObject racket_spawner;
    public GameObject creds;
    public GameObject tut;
    public GameObject winscreen;
    public GameObject loosescreen;
    public GameObject gamemode;
    public GameObject endlesswalls;
    public GameObject normalwalls;
    public GameObject endlessspawns;
    public GameObject normalspawns;
    //SteamVR_Action_Boolean menu = SteamVR_Input.GetBooleanAction("Menu");
    public GameObject Plr;
    public Text score;

    void Start()
    {
        chaosmeter = GetComponent<ChaosMeter>();
        spawn_ball();
        pause();
    }

    public void getballback()
    {
        foreach (GameObject ball in BALLS)
        {
            //ball.GetComponent<LockToPoint>().works = true;
        }
    }

    public void restart()
    {
        chaosmeter.initchaos = 0;
        chaosmeter.chaos = 0;
        Destroy(Plr);
        SceneManager.LoadScene("TheGaem");
    }

    public void Victory()
    {
        pause();
        UI_board.SetActive(true);
        winscreen.SetActive(true);
    }

    public void Defeat()
    {
        pause();
        UI_board.SetActive(true);
        loosescreen.SetActive(true);
        score.text = "But you have created " + Mathf.Round(chaosmeter.chaos) + " Chaos";
    }

    public void idgaf()
    {
        creds.SetActive(false);
        tut.SetActive(true);
    }

    public void idgaf2()
    {
        tut.SetActive(false);
        righthandedtablet.SetActive(true);
    }

    public void open_menu()
    {
        pause();
        UI_board.SetActive(true);
        Menu.SetActive(true);
    }

    public void exit()
    {
        Application.Quit();
    }

    public void gaem_continue()
    {
        unpause();
        UI_board.SetActive(false);
        Menu.SetActive(false);
    }

    public void hand_choice(int choice)
    {
        righhanded = choice;
        foreach (GameObject ball in ball_spawner)
        {
            ball.transform.localPosition = new Vector3(ball.transform.localPosition.x * righhanded, ball.transform.localPosition.y, ball.transform.localPosition.z);
        }
        racket_spawner.transform.position = new Vector3(racket_spawner.transform.position.x * righhanded, racket_spawner.transform.position.y, racket_spawner.transform.position.z);
        righthandedtablet.SetActive(false);
        gamemode.SetActive(true);
    }

    public void gamemode_choice(bool endless)
    {
        chaosmeter.endless = endless;
        endlessspawns.SetActive(endless);
        normalspawns.SetActive(!endless);
        endlesswalls.SetActive(endless);
        normalwalls.SetActive(!endless);
        UI_board.SetActive(false);
        gamemode.SetActive(false);
        unpause();
    }

    public void make_explosive()
    {
        explosive = true;
        BALLS.ForEach(delegate (GameObject ball)
        {
            ball.GetComponent<Chaining_and_comeback>().explosive = true;
        });
    }

    public void need_upgrade()
    {
        StartCoroutine(Upgrade());
    }

    public void pluschain()
    {
        chain_length += 1;
        BALLS.ForEach(delegate (GameObject ball)
        {
            ball.GetComponent<Chaining_and_comeback>().chain_length = chain_length;
        });
    }

    IEnumerator Upgrade()
    {
        yield return new WaitForSecondsRealtime(1);
        pause();
        UI_board.SetActive(true);
        Upgrades.SetActive(true);
    }

    public void shut()
    {
        UI_board.SetActive(false);
        Upgrades.SetActive(false);
        unpause();
    }

    private void pause()
    {
        chaosmeter.pause = true;
        /*
        GameObject[] destructibles = GameObject.FindGameObjectsWithTag("Destructible");
        dest_speeds = new Vector3[destructibles.Length];
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Bol");
        bol_speeds = new Vector3[balls.Length];
        for (int i = 0; i < destructibles.Length; i++)
        {
            dest_speeds[i] = destructibles[i].GetComponent<Rigidbody>().velocity;
            destructibles[i].GetComponent<Rigidbody>().isKinematic = true;
        }
        for (int i = 0; i < balls.Length; i++)
        {
            bol_speeds[i] = balls[i].GetComponent<Rigidbody>().velocity;
            balls[i].GetComponent<Rigidbody>().isKinematic = true;
        }
        */
        stoppable[] all = FindObjectsOfType<stoppable>();
        foreach (stoppable dude in all)
        {
            dude.pause();
        }

        /*
        BALLS.ForEach(delegate (GameObject ball)
        {
            ball.SetActive(false);
        });
        */

        chaosmeter.chaotic_music.Stop();
        chaosmeter.chill_music.Stop();
        chaosmeter.chaotic_music.PlayOneShot(pause_snd, 0.6f);
    }

    private void unpause()
    {
        chaosmeter.pause = false;
        /*
        GameObject[] destructibles = GameObject.FindGameObjectsWithTag("Destructible");
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Bol");
        for (int i = 0; (i < destructibles.Length) && (i < dest_speeds.Length) ; i++)
        {
            destructibles[i].GetComponent<Rigidbody>().isKinematic = false;
            destructibles[i].GetComponent<Rigidbody>().velocity = dest_speeds[i];
        }
        for (int i = 0; i < bol_speeds.Length; i++)
        {
            balls[i].GetComponent<Rigidbody>().isKinematic = false;
            balls[i].GetComponent<Rigidbody>().velocity = bol_speeds[i];
        }
        */
        /*
        BALLS.ForEach(delegate (GameObject ball)
        {
            ball.SetActive(true);
        });
        */

        stoppable[] all = FindObjectsOfType<stoppable>();
        foreach (stoppable dude in all)
        {
            dude.unpause();
        }

        /*
        chaosmeter.chaotic_music.Play();
        chaosmeter.chill_music.Play();
        */
    }

    public void spawn_ball()
    {
        GameObject spwnd = Instantiate(ball_obj);
        spwnd.GetComponent<Chaining_and_comeback>().chain_length = chain_length;
        foreach (GameObject ball in BALLS)
        {
            Physics.IgnoreCollision(spwnd.GetComponentInChildren<Collider>(), ball.GetComponentInChildren<Collider>(), true);
        }
        BALLS.Add(spwnd);
        spwnd.GetComponent<LockToPoint>().snapTo = ball_spawner[numofballs].transform;
        spwnd.GetComponent<Chaining_and_comeback>().player = ball_spawner[numofballs];
        numofballs++;
        spwnd.GetComponent<Chaining_and_comeback>().explosive = explosive;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (menu.GetStateDown(SteamVR_Input_Sources.LeftHand) && (Menu.activeSelf || !UI_board.activeSelf))
        {
            if (chaosmeter.pause)
            {
                gaem_continue();
            }
            else
            {
                open_menu();
            }
        }
        */
    }

    private void LateUpdate()
    {
        if ((chaosmeter.chaos >= 100) && !chaosmeter.endless)
        {
            Victory();
        }
        if ((chaosmeter.chaos < chaosmeter.initchaos) && (chaosmeter.initchaos > 1))
        {
            Defeat();
        }
    }
}
