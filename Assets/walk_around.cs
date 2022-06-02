using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
    public class walk_around : MonoBehaviour
    {
        public float spd = 1000;
        public Transform player;
        /*
        public SteamVR_Action_Single Forward = SteamVR_Input.GetSingleAction("Forward");
        public SteamVR_Action_Single Back = SteamVR_Input.GetSingleAction("Back");
        public SteamVR_Action_Single Left = SteamVR_Input.GetSingleAction("Left");
        public SteamVR_Action_Single Right = SteamVR_Input.GetSingleAction("Right");
        */
        public SteamVR_Action_Vector2 Movement = SteamVR_Input.GetVector2Action("Movement");

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            /*
            float fw = Forward.GetAxis(SteamVR_Input_Sources.LeftHand);
            float bc = Back.GetAxis(SteamVR_Input_Sources.LeftHand);
            float lf = Left.GetAxis(SteamVR_Input_Sources.LeftHand);
            float rt = Right.GetAxis(SteamVR_Input_Sources.LeftHand);
            */
            Vector2 m = Movement.GetAxis(SteamVR_Input_Sources.LeftHand);
            //player.position += new Vector3((fw - bc) * spd * Time.deltaTime, 0, (rt - lf) * spd * Time.deltaTime);
            //Debug.Log(fw + " " + bc + " " + lf + " " + rt);
            //Debug.Log(new Vector3((fw - bc) * spd * Time.deltaTime, 0, (rt - lf) * spd * Time.deltaTime
            Vector3 move = transform.rotation * new Vector3(m.x * spd, 0, m.y * spd);
            player.position += new Vector3(move.x, 0, move.z);
            if (Mathf.Abs(player.position.x) > 2.5f)
            {
                player.position = new Vector3(2.5f * Mathf.Sign(player.position.x), player.position.y, player.position.z);
            }
            if (Mathf.Abs(player.position.z) > 2.5f)
            {
                player.position = new Vector3(player.position.x, player.position.y, 2.5f * Mathf.Sign(player.position.z));
            }

        }
    }
}