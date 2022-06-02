using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem.Sample
{
    public class Upgrade_system : MonoBehaviour
    {
        public GameObject mommy;
        private Initiate_gaem init;
        public GameObject explosive_button;
        public GameObject time_button;
        // Start is called before the first frame update
        void Start()
        {
            init = mommy.GetComponent<Initiate_gaem>();
        }

        public void explosive_upgrade()
        {
            init.make_explosive();
            explosive_button.SetActive(false);
            time_button.SetActive(true);
            init.shut();
        }

        public void persistance_upgrade()
        {
            mommy.GetComponent<ChaosMeter>().slow_down();
            init.shut();
        }

        public void chain_upgrade()
        {
            init.pluschain();
            init.shut();
        }

        public void ball_upgrade()
        {
            init.spawn_ball();
            init.shut();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
