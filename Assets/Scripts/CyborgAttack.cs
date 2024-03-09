using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyborgAttack : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] GameObject player;
    [SerializeField] float timeBetweenEachProjectile = 2f;
    [SerializeField] float triggerDistance = 5f;

    MeshRenderer cyborgEnemyMesh;
    MeshRenderer cyborgWeaponMesh;
    float elapsedTime = 0f;
    float distanceToPlayer;

    void Start()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        cyborgEnemyMesh = GameObject.FindGameObjectWithTag("CyborgEnemyMesh").GetComponent<MeshRenderer>();
        cyborgWeaponMesh = GameObject.FindGameObjectWithTag("CyborgWeaponMesh").GetComponent<MeshRenderer>();
    }

    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        elapsedTime += Time.deltaTime;

        if(distanceToPlayer <= triggerDistance)
        {
            if(elapsedTime >= timeBetweenEachProjectile) 
            {
                Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                elapsedTime = 0f;
            }
        }
    }
}
