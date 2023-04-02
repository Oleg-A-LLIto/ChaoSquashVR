using UnityEngine;
using System.Collections;
public class ParticleSpeed : MonoBehaviour
{
    public float speed = 2;
    ParticleSystem ps;
    void Start() { ps = GetComponent<ParticleSystem>() as ParticleSystem; }
    void Update() { ps.playbackSpeed = speed; } 
}