using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpStrength = 10f;

    Rigidbody rb;
    bool onGround;
    Transform checkGround;
    LayerMask groundMask;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        checkGround = transform.Find("CheckGround");
        groundMask = LayerMask.GetMask("Ground");
    }

    void Update()
    {
        Collider[] collidingFloor = Physics.OverlapSphere(checkGround.position, 0.2f, groundMask);
        onGround = collidingFloor.Length > 0;

        float horizontalMovement = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontalMovement * speed, rb.velocity.y);
        
        
        if (onGround && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector2.up * jumpStrength, ForceMode.Impulse);
        }
    }
}
