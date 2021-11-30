using System;
using UnityEngine;


public class ShootAtPlayer : EnemyBehaviour
{
    [SerializeField] Gun gun;

    void Update()
    {
        var player_center = GameManager.Instance.Player.GetComponent<Collider2D>().bounds.center;
        Vector3 direction = player_center - transform.position;
        Debug.DrawRay(transform.position, direction, Color.green);
        gun.Shoot(direction);
    }
}