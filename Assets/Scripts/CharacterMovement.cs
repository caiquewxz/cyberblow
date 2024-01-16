using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpStrength = 10f;

    Rigidbody2D rb;
    bool onGround;
    Transform checkGround;
    LayerMask groundMask;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        checkGround = transform.Find("CheckGround");
        groundMask = LayerMask.GetMask("Ground");
    }

    void Update()
    {
        onGround = Physics2D.OverlapCircle(checkGround.position, 0.2f, groundMask);

        float horizontalMovement = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontalMovement * speed, rb.velocity.y);

        if (onGround && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector2.up * jumpStrength, ForceMode2D.Impulse);
        }
    }
}
