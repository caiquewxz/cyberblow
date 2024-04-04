using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootComponent : MonoBehaviour
{
    [SerializeField] Transform firePoint;
    [SerializeField] float impulseForce = 10f;
    [SerializeField] GameObject particlePrefab;
    [SerializeField] float mouseTimePressed = 0f;
    public MeshRenderer playerRenderer;

    float impulseParameter = 0f;

    public Projectile bulletPrefab;

    Rigidbody rb;

    Animator animator;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        

        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            mouseTimePressed = 0f;
            Vector3 bulletRotation = GetBulletRotation();
            Projectile spawnedProjectile = Shoot(bulletRotation);
            ThrowCharacter(bulletRotation, impulseParameter * spawnedProjectile.impulseFactor);
        }
        
        if (Input.GetKey(KeyCode.Mouse0))
        {
            mouseTimePressed += Time.deltaTime;

            if(mouseTimePressed < 1f)
            {
                impulseParameter = 1f;
            }
            else if (mouseTimePressed > 1.5f) 
            {
                impulseParameter = 10f;
            }
            else if (mouseTimePressed > 3f)
            {
                impulseParameter = 20f;
            }

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

    public Projectile Shoot(Vector3 bulletRotation)
    {
        PlayShootingAnimation();
        SpawnShootParticle();
        Projectile newProjectile = Instantiate<Projectile>(bulletPrefab, firePoint.position, Quaternion.identity);
        newProjectile.direction = bulletRotation;

        return newProjectile;
    }

    void ThrowCharacter(Vector3 direction, float timePressed)
    {
        rb.AddForce(direction * -1 * impulseForce * timePressed, ForceMode.Impulse);
        impulseParameter = 0f;
    }

    void PlayShootingAnimation()
    {
        animator?.SetTrigger("Shoot");
    }

    void SpawnShootParticle()
    {
        if (particlePrefab != null)
        {
            var copy = Instantiate(particlePrefab, firePoint);
            Instantiate(particlePrefab, firePoint);
            Destroy(copy, 3f);
        }
    }

}
