using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyborgProjectile : Projectile
{
    [SerializeField] float lerpSpeed = 2f;

    Vector3 initialScale = Vector3.zero;
    Vector3 finalScale = Vector3.one;

    float alpha;

    bool isLerpFinished = false;


    void Start()
    {
        isLerpFinished = false;
    }

    protected override void Update()
    {
        if (!isLerpFinished)
        {
            alpha += Time.deltaTime * lerpSpeed;
            transform.localScale = Vector3.Lerp(initialScale, finalScale, alpha);
        }

        if(alpha >= 1f)
        {
            isLerpFinished = true;
        }

        if (isLerpFinished)
        {
            transform.position += transform.forward * base.projectileSpeed * Time.deltaTime;
            Destroy(gameObject, 3f);
        }
    }

    protected override void OnCollisionEnter(Collision other)
    {
        
    }
}
