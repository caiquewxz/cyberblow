using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootComponent : MonoBehaviour
{
    [SerializeField] Transform firePoint;
    [SerializeField] Projectile bulletPrefab;
    [SerializeField] float impulseForce = 10f;
    [SerializeField] GameObject particlePrefab;

    Rigidbody rb;

    Animator animator;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Vector3 bulletRotation = GetBulletRotation();
            Shoot(bulletRotation);
            ThrowCharacter(bulletRotation);
        }
    }

    Vector3 GetBulletRotation()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = transform.position.z;
        Vector3 direction = (mousePos - transform.position).normalized;
        Debug.DrawRay(transform.position, direction);
        return direction;
    }

    public void Shoot(Vector3 bulletRotation)
    {
        PlayShootingAnimation();
        SpawnShootParticle();
        Projectile newProjecile = Instantiate<Projectile>(bulletPrefab, firePoint.position, Quaternion.identity);
        newProjecile.direction = bulletRotation;

    }

    void ThrowCharacter(Vector3 direction)
    {
        rb.AddForce(direction * -1 * impulseForce, ForceMode.Impulse);
        Debug.Log(direction * -1 * impulseForce);
    }

    void PlayShootingAnimation()
    {
        animator?.SetTrigger("Shoot");
    }

    void SpawnShootParticle()
    {
        if (particlePrefab != null)
        {
            Instantiate(particlePrefab, firePoint);
            Destroy(particlePrefab, 1f);
        }
    }

}
