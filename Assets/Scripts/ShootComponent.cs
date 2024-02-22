using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootComponent : MonoBehaviour
{
    [SerializeField] Transform firePoint;
    [SerializeField] float impulseForce = 10f;
    [SerializeField] GameObject particlePrefab;
    [SerializeField] bool isMousePressed = false;
    [SerializeField] float mouseTimePressed = 0f;

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

        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            Debug.Log(impulseParameter);
            mouseTimePressed = 0f;
            Vector3 bulletRotation = GetBulletRotation();
            Shoot(bulletRotation);
            ThrowCharacter(bulletRotation, impulseParameter);
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

    void ThrowCharacter(Vector3 direction, float timePressed)
    {
        rb.AddForce(direction * -1 * impulseForce * timePressed, ForceMode.Impulse);
        Debug.Log(direction * -1 * impulseForce);
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
            Instantiate(particlePrefab, firePoint);
            Destroy(particlePrefab, 1f);
        }
    }

}
