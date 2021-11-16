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

    GameObject CreateProjectile(Vector2 direction)
    {
        GameObject bullet = Instantiate(bulletPrefab, _barrel.position, Quaternion.identity);
        bullet.GetComponent<Projectile>().Init(speed, direction);
        return bullet;
    }

    public void Shoot(Vector2 direction)
    {
        if (_canShoot)
        {
            CreateProjectile(direction);
            StartCoroutine(CooldownRoutine());
        }
    }

    public void Spread(int angleStep)
    {
        if (_canShoot)
        {
            var sectors = 360 / angleStep;
            for (int i = 0; i < sectors; i++)
            {
                Vector2 dir = Quaternion.Euler(0, 0, i * angleStep) * Vector2.up;
                CreateProjectile(dir);
            }
            StartCoroutine(CooldownRoutine());
        }
    }
}
