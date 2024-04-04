using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    [SerializeField] Transform player;

    [SerializeField] private Transform aimReference;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
                Vector2 mousePos = Input.mousePosition;
        
                Vector2 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
        
                Vector2 mouseDirection = worldMousePos - (Vector2)player.position;
        
                float angle = Vector2.Angle(Vector2.up, mouseDirection);
        
                if (mouseDirection.x < 0)
                {
                    angle = 360f - angle;
                }

                Debug.Log("Mouse Angle " + angle);
                aimReference.localRotation = Quaternion.Euler(0, 0,-angle);
    }
}
