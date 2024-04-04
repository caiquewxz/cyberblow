using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableAmmo : MonoBehaviour
{
    [SerializeField] Projectile bulletPrefab;
    [SerializeField] GameObject player;
    [SerializeField] private Material bulletMaterial;

    ShootComponent shootComponent;

    void Start()
    {
        if(player == null)
        {
            player = CharacterMovement.instance.gameObject;
        }

        shootComponent = player.GetComponent<ShootComponent>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            shootComponent.bulletPrefab = bulletPrefab;
            shootComponent.playerRenderer.SetMaterials(new List<Material>(){ bulletMaterial });
            Destroy(this.gameObject);
        }
    }
}
