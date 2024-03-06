using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyborgAttack : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float timeBetweenEachProjectile = 2f;

    float elapsedTime = 0f;

    void Start()
    {
        Instantiate(projectilePrefab);
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        if(elapsedTime >= timeBetweenEachProjectile) 
        {
            Instantiate(projectilePrefab);
            elapsedTime = 0f;
        }
    }
}
