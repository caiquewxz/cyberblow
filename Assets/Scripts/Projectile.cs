using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Projectile : MonoBehaviour
{
    [SerializeField] float lifeTime = 2f;
    [SerializeField] GameObject trailPrefab;
    [SerializeField] protected float damage = 10;

    public float projectileSpeed = 10f;
    public Vector3 direction;

    public float impulseFactor = 1f;
    

    protected virtual void Start()
    {
        

        if (trailPrefab != null)
        {
            Instantiate(trailPrefab, this.gameObject.transform);
        }

        Destroy(gameObject, lifeTime);
    }

    protected virtual void Update()
    {
        transform.position += (direction * projectileSpeed * Time.deltaTime);
    }

    protected virtual void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collided with: " + other.gameObject.name);
        if (other.gameObject.CompareTag("Plane") || other.gameObject.layer == LayerMask.GetMask("ground"))
        {
            Vector3 normal = other.contacts[0].normal;
            direction = Vector3.Reflect(direction, normal).normalized;
        }

    }

    protected virtual void BeforeDestroy()
    {
        
    }
}
