using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBalls : MonoBehaviour
{
    [SerializeField] GameObject ProjectilePrefab;
    [SerializeField] Transform GunOrigin;
    [SerializeField] Transform ShootingPoint;
    [SerializeField] float maxPower;
    [SerializeField] float maxPowerTime;
    bool charging = false;
    float currentCharge = 0.0f;

    void Start()
    {
        
    }

    public void StartCarging()
    {
        charging = true;
    }

    public void Release()
    {
        charging = false;
        float _finalCharge = Mathf.Min(maxPowerTime, currentCharge) / maxPowerTime;
        GameObject _projectile = Object.Instantiate(ProjectilePrefab);
        _projectile.transform.position = ShootingPoint.position;
        _projectile.GetComponent<Rigidbody>().AddExplosionForce(maxPower * _finalCharge, GunOrigin.position, 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (charging)
            currentCharge += Time.deltaTime;
    }
}
