using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyborgProjectile : MonoBehaviour
{
    [SerializeField] float lerpSpeed = 2f;
    [SerializeField] float projectileSpeed = 10f;

    Vector3 initialScale = Vector3.zero;
    Vector3 finalScale = Vector3.one;

    Transform transform;

    float alpha;

    bool isLerpFinished = false;


    void Start()
    {
        isLerpFinished = false;
        transform = GetComponent<Transform>();
    }

    void Update()
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
            transform.position += transform.forward * projectileSpeed * Time.deltaTime;
            Destroy(gameObject, 3f);
        }
    }
}
