using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float lifeTime = 2f;
    [SerializeField] GameObject trailPrefab;

    public Vector3 direction;

    void Start()
    { 
        if(trailPrefab != null)
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
        if(other.gameObject.CompareTag("Plane"))
        {
            Debug.Log("colidiu");
            Vector3 normal = other.contacts[0].normal;
            Debug.Log("Direction: " + direction);
            direction = Vector3.Reflect(direction, normal).normalized;
            Debug.Log("New Direction: " + direction);
        }
    }
}
