using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableAmmo : MonoBehaviour
{
    [SerializeField] Projectile bulletPrefab;
    [SerializeField] GameObject player;

    ShootComponent shootComponent;

    void Start()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        shootComponent = player.GetComponent<ShootComponent>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            shootComponent.bulletPrefab = bulletPrefab;
            Destroy(this.gameObject);
        }
    }
}
