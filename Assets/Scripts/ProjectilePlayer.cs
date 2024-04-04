using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePlayer : Projectile
{
    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        if (collision.gameObject.CompareTag("Enemy"))
        {
            HealthComponent enemyHealth = collision.gameObject.GetComponent<HealthComponent>();
            enemyHealth?.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}