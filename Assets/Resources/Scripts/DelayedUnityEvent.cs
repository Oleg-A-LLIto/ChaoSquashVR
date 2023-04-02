using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DelayedUnityEvent: MonoBehaviour
{
    [SerializeField] private UnityEvent Event;
    [SerializeField] private float timeout;


    // Start is called before the first frame update
    public void Invoke(float _t = -1)
    {
        if(_t == -1)
        {
            _t = timeout;
        }

        Invoke("ActualEvent", _t);
    }

    private void ActualEvent()
    {
        Event.Invoke();
    }
}
