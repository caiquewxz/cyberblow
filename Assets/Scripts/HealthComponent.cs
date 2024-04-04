using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour
{
    public float Health;
    public float MaxHealth = 100;
    public UnityEvent OnDie;
    //public bool isDead;

    [SerializeField] private Transform player;
    [SerializeField] private Transform startPoint;

    private CheckPoint checkPoint;

    void Start()
    {
        Health = MaxHealth;

        if (player == null)
        {
            player = CharacterMovement.instance.transform;
        }

        checkPoint = player.GetComponent<CheckPoint>();
    }

    public void TakeDamage(float damage)
    {
        //if(isDead) return;

        Health -= damage;

        if(Health <= 0)
        {
            Health = 0;
            OnDie.Invoke();
            //isDead = true;
        }
    }

    public void DestroyItself()
    {
        Destroy(gameObject); 
    }

    public void RestartStage()
    {
        if (startPoint != null)
        {
            player.position = checkPoint.currentCheckPoint;
        }

        //isDead = false;
        Health = MaxHealth;
    }
}
