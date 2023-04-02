using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Autohand;

namespace Valve.VR.InteractionSystem.Sample
{
    public class vibrate_on_impact : MonoBehaviour
    {
        Hand handy = null;
        // Start is called before the first frame update
        void Start()
        {

        }

        protected virtual void OnDetachedFromHand(Hand hand)
        {
            handy = null;
        }

        protected virtual void OnAttachedToHand(Hand hand)
        {
            handy = hand;
        }

        // Update is called once per frame
        private void OnCollisionExit(Collision collision)
        {
            if (handy != null)
            {
                if (collision.collider.CompareTag("Bol"))
                {
                    float pulse = collision.relativeVelocity.magnitude/20;
                    //Debug.Log("Pulse: " + pulse);
                    
                    //handy.TriggerHapticPulse((ushort)pulse);
                    GetComponent<AudioSource>().pitch = Random.Range(0.8f, 1.2f);
                    GetComponent<AudioSource>().volume = pulse;
                    GetComponent<AudioSource>().Play();
                }
                
                //SteamVR_Controller.Input( (int)trackedObject.index ).TriggerHapticPulse( (ushort)pulse );
            }
            
        }

        void Update()
        {
            
        }
    }
}
