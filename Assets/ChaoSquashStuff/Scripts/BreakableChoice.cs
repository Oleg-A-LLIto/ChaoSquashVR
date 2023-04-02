using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BreakableChoice : MonoBehaviour
{
    [SerializeField] GameObject Shards;
    [SerializeField] UnityEvent OnBroken;

    public void Choose(GameObject _plate, float _impact)
    {
        Shards.SetActive(true);
        _plate.SetActive(false);
        foreach(Rigidbody _shard in Shards.GetComponentsInChildren<Rigidbody>())
        {
            _shard.AddExplosionForce(_impact, _plate.transform.position, 2);
        }
        OnBroken.Invoke();
    }
}
