using System;
using System.Collections;
using UnityEngine;


public class SpreadShoot : MonoBehaviour
{
    [SerializeField] Gun gun;
    [SerializeField] int angleStep;
    
    void Update()
    {
        gun.Spread(angleStep);
    }
}