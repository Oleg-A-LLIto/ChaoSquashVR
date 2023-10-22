using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BreakableChoice : MonoBehaviour
{
    [SerializeField] GameObject Shards;
    [SerializeField] UnityEvent OnBroken;
    [SerializeField] float explodePower = 0.1f;

    public void Choose(GameObject _plate, float _impact)
    {
        Shards.SetActive(true);
        _plate.SetActive(false);
        foreach(Rigidbody _shard in Shards.GetComponentsInChildren<Rigidbody>())
        {
            _shard.transform.SetParent(null);
            _shard.AddForce((_impact) * (_shard.position - transform.position).normalized * explodePower, ForceMode.Impulse);
        }
        OnBroken.Invoke();
    }
}
