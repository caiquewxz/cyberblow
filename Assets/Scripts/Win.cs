using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Win : MonoBehaviour
{
    [SerializeField] private string winText = "You win!";
    private Text winTextComponent;
    
    void Start()
    {
        winTextComponent = GameObject.FindGameObjectWithTag("WinText").GetComponent<Text>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            WinGame();
        }
    }

    private void WinGame()
    {
        winTextComponent.text = winText;
        Time.timeScale = 0f;
    }
}
