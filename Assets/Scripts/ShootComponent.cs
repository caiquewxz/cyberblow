using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootComponent : MonoBehaviour
{
    [SerializeField] Transform firePoint;
    [SerializeField] Projectile bulletPrefab;
    [SerializeField] float impulseForce = 10f;

    Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
        Projectile newProjecile = Instantiate<Projectile>(bulletPrefab, firePoint.position, Quaternion.identity);
        newProjecile.direction = bulletRotation;

    }

    void ThrowCharacter(Vector3 direction)
    {
        rb.AddForce(direction * -1 * impulseForce, ForceMode.Impulse);
        Debug.Log(direction * -1 * impulseForce);
    }


}
