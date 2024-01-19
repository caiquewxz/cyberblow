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
}
