using UnityEngine;
using System.Collections;

public class Turret : Character
{
    public Projectile projectilePrefab;

    public float fireRate = 1;
    public int damage = 1;


    protected override void Start()
    {
        float timeBetweenShots = 1f / fireRate;

        InvokeRepeating("Fire", timeBetweenShots, timeBetweenShots);
    }

    void Fire()
    {
        if (projectilePrefab)
        {
            Projectile proj = Instantiate<Projectile>(projectilePrefab);
            proj.transform.position = transform.position;
            proj.DamageAuthor = gameObject;
            proj.damage = damage;
        }
    }
}
