using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;

    [Header("Bullet props")] 
    [SerializeField] float speed;
    [SerializeField] float cooldown;

    Transform _barrel;
    bool _canShoot = true;
    
    void Start()
    {
        _barrel = transform.Find("Barrel").transform;
    }

    IEnumerator CooldownRoutine()
    {
        _canShoot = false;
        yield return new WaitForSeconds(cooldown);
        _canShoot = true;
    }

    public void Shoot(Vector2 direction)
    {
        if (_canShoot)
        {
            GameObject bullet = Instantiate(bulletPrefab, _barrel.position, Quaternion.identity);
            bullet.GetComponent<Projectile>().Init(speed, direction);
            StartCoroutine(CooldownRoutine());
        }
    }
}
