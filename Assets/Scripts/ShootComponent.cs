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
    [SerializeField] private float shootDelay = 1.5f;
    public MeshRenderer playerRenderer;
    public bool canShoot;
    public Projectile bulletPrefab;

    float impulseParameter = 0f;
    private float shootCooldown;

    Rigidbody rb;

    Animator animator;


    void Start()
    {
        canShoot = true;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (shootCooldown < shootDelay)
        {
            shootCooldown += Time.deltaTime;
            canShoot = false;
        }
        else if (shootCooldown >= shootDelay)
        {
            shootCooldown = shootDelay;
            canShoot = true;
        }
        
        if(Input.GetKeyUp(KeyCode.Mouse0) && canShoot)
        {
            mouseTimePressed = 0f;
            Vector3 bulletRotation = GetBulletRotation();
            Projectile spawnedProjectile = Shoot(bulletRotation);
            ThrowCharacter(bulletRotation, impulseParameter * spawnedProjectile.impulseFactor);
        }
        
        if (Input.GetKey(KeyCode.Mouse0))
        {
            mouseTimePressed += Time.deltaTime;

            if(mouseTimePressed < .5f)
            {
                impulseParameter = 1f;
            }
            else if (mouseTimePressed > 1f) 
            {
                impulseParameter = 5f;
            }
            else if (mouseTimePressed > 2f)
            {
                impulseParameter = 10f;
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
        Projectile newProjectile = null;
        
        if (canShoot)
        {
            PlayShootingAnimation();
            SpawnShootParticle();
            newProjectile = Instantiate<Projectile>(bulletPrefab, firePoint.position, Quaternion.identity);
        }
        newProjectile.direction = bulletRotation;
        shootCooldown = 0;
        canShoot = false;
        
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
