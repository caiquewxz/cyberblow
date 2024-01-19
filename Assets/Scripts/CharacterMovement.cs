using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpStrength = 10f;
    [SerializeField] Transform playerMesh;

    Rigidbody rb;
    bool onGround;
    Transform checkGround;
    LayerMask groundMask;

    Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        checkGround = transform.Find("CheckGround");
        groundMask = LayerMask.GetMask("Ground");
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Collider[] collidingFloor = Physics.OverlapSphere(checkGround.position, 0.2f, groundMask);
        onGround = collidingFloor.Length > 0;
        animator.SetBool("InAir", !onGround);

        float horizontalMovement = -Input.GetAxis("Horizontal");
        

        Vector3 desiredMovement = new Vector3(horizontalMovement * speed * Time.deltaTime, 0, 0);
        transform.Translate(desiredMovement);

        
        if (onGround && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector2.up * jumpStrength, ForceMode.Impulse);
        }

        RotationControl();
    }

    void RotationControl()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePos.x > transform.position.x)
        {
            TurnRight();
        }
        else if(mousePos.x < transform.position.x)
        {
            TurnLeft();
        }
    }

    void TurnRight()
    {
        playerMesh.transform.rotation = Quaternion.Euler(0, 90, 0);

    }

    void TurnLeft()
    {
        playerMesh.transform.rotation = Quaternion.Euler(0, -90, 0);
    }
}
