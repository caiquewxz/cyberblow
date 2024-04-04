using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathOnFall : MonoBehaviour
{
    private HealthComponent healthComponent;
    void Start()
    {
        healthComponent = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthComponent>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            healthComponent.OnDie.Invoke();
        }
    }
}
