using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Ricochet : MonoBehaviour
{
    [SerializeField] bool canTpOnRicochet = false;

    Teleport teleportScript;
    Projectile projectile;
    Vector3 direction;

    private void Start()
    {
        Teleport teleport = GetComponent<Teleport>();
        projectile = GetComponent<Projectile>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Plane"))
        {
            Debug.Log(other);
            direction = Vector3.Reflect(-direction, other.transform.forward);
            projectile.direction = direction;

            if (canTpOnRicochet)
            {
                teleportScript.TeleportToBulletCollision(transform.position);
            }
        }
    }
}
