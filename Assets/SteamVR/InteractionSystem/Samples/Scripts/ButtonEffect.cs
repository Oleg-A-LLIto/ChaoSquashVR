//======= Copyright (c) Valve Corporation, All rights reserved. ===============

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

namespace Valve.VR.InteractionSystem.Sample
{
    public class ButtonEffect : MonoBehaviour
    {
        public void OnButtonDown(Hand fromHand)
        {
            ColorSelf(new Vector4(1,0,0,1));
            fromHand.TriggerHapticPulse(1000);
            GetComponent<AudioSource>().Play();
        }

        public void OnButtonUp(Hand fromHand)
        {
            ColorSelf(new Vector4(0.2f, 0.2f, 0.2f, 1));
        }

        private void ColorSelf(Color newColor)
        {
            Renderer[] renderers = this.GetComponentsInChildren<Renderer>();
            for (int rendererIndex = 0; rendererIndex < renderers.Length; rendererIndex++)
            {
                renderers[rendererIndex].material.color = newColor;
            }
        }
    }
}