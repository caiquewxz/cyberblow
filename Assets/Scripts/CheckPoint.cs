using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Vector3 currentCheckPoint;
    
    void Start()
    {
        currentCheckPoint = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CheckPoint"))
        {
            currentCheckPoint = other.transform.position;
            Destroy(other.gameObject);
        }
    }
}
