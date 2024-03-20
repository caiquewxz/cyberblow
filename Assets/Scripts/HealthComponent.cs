using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour
{
    public float Health;
    public float MaxHealth = 100;
    public UnityEvent OnDie;
    public bool isDead;

    void Start()
    {
        Health = MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        if(isDead) return;

        Health -= damage;

        if(Health <= 0)
        {
            Health = 0;
            OnDie.Invoke();
            isDead = true;
        }
    }

    public void DestroyItself()
    {
        Destroy(gameObject); 
    }
}
