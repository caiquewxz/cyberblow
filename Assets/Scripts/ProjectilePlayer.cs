using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePlayer : Projectile
{
    [SerializeField] GameObject projectileDestroySfx;

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

    private void OnDestroy()
    {
        Instantiate(projectileDestroySfx);
    }
}