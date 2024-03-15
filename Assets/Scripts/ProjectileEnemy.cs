using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileProjectile : Projectile
{
    protected override void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("player");
            HealthComponent playerHealth = other.gameObject.GetComponent<HealthComponent>();
            playerHealth?.TakeDamage(damage);
            Destroy(gameObject);
            Debug.Log("Player is Dead");
        }
    }
}
