using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] bool canTpOnRicochet = false;
    [SerializeField] float speed = 10f;
    [SerializeField] float lifeTime = 2f;
    [SerializeField] GameObject trailPrefab;

    public Vector3 direction;
    Teleport teleportScript;

    void Start()
    {
        teleportScript = GetComponent<Teleport>();

        if (trailPrefab != null)
        {
            Instantiate(trailPrefab, this.gameObject.transform);
        }

        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.position += (direction * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Plane") || other.gameObject.layer == LayerMask.GetMask("ground"))
        {
            Debug.Log("colidiu");
            Vector3 normal = other.contacts[0].normal;
            direction = Vector3.Reflect(direction, normal).normalized;
        }

        if (canTpOnRicochet)
        {
            teleportScript.TeleportToBulletCollision(transform.position);
            Destroy(gameObject);
        }
    }
}
