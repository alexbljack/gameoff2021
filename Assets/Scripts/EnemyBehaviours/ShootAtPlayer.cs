using System;
using UnityEngine;


public class ShootAtPlayer : MonoBehaviour
{
    [SerializeField] Gun gun;
    
    void Update()
    {
        Vector3 direction = GameManager.Instance.Player.position - transform.position;
        gun.Shoot(direction);
    }
}