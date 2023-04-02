using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class Fading : MonoBehaviour
{
    private CanvasGroup CG;
    private Coroutine Animation;
    private bool fadingIn;
    public float Duration = 1;
    
    // Start is called before the first frame update
    void Awake()
    {
        Animation = null;
        CG = GetComponent<CanvasGroup>();
    }
    public void Fade(bool _fadeIn)
    {
        if(Animation != null)
        {
            StopCoroutine(Animation);
        }
        fadingIn = _fadeIn;
        Animation = StartCoroutine(IFade());
    }

    private IEnumerator IFade()
    {
        for(float i = 0; i < Duration; i += Time.deltaTime)
        {
            if (fadingIn)
                CG.alpha = i / Duration;
            else
                CG.alpha = 1 - (i / Duration);
            yield return 0;
        }
        if (fadingIn)
            CG.alpha = 1;
        else
            CG.alpha = 0;
        Animation = null;
    }
}
