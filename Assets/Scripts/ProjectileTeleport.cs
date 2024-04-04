using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTeleport : Projectile
{
    private Vector3 oldLocalPosition;
    Teleport teleportScript;
    private ShootComponent shootComponent;
    
    protected override void Start()
    {
        base.Start();
        oldLocalPosition = Camera.main.transform.localPosition;

        Camera.main.transform.parent = transform;
        teleportScript = GetComponent<Teleport>();

        shootComponent = CharacterMovement.instance.GetComponent<ShootComponent>();

        shootComponent.canShoot = false;
    }

    protected override void BeforeDestroy()
    {
        Camera.main.transform.parent = CharacterMovement.instance.transform;
        Camera.main.transform.localPosition = oldLocalPosition;
        shootComponent.canShoot = true;
    }
    protected override void OnCollisionEnter(Collision other)
    {
        Debug.Log("Trigger Enter");
        teleportScript.TeleportToBulletCollision(transform.position);
        BeforeDestroy();
        Destroy(gameObject);
    }
}
