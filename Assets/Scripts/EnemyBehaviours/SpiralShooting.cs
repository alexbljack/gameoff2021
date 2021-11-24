using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralShooting : EnemyShootBehaviour
{
    [SerializeField] float rotationSpeed;
    [SerializeField] Gun gun;
    
    void Update()
    {
        gun.transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        gun.Shoot(gun.transform.right);
    }
}
