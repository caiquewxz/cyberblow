using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpStrength = 10f;
    [SerializeField] Transform playerMesh;
    [SerializeField] float maxSpeed = 80;

    Rigidbody rb;
    bool onGround;
    Transform checkGround;
    LayerMask groundMask;
    AudioSource jumpSfx;

    Animator animator;

    public static CharacterMovement instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<CharacterMovement>();
            }

            return _instance;
        }
    }
    private static CharacterMovement _instance;

    void Start()
    {
        if (instance != this)
        {
            Destroy(gameObject);
        }
        jumpSfx = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        checkGround = transform.Find("CheckGround");
        groundMask = LayerMask.GetMask("Ground");
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        
        Collider[] collidingFloor = Physics.OverlapSphere(checkGround.position, 0.2f, groundMask);
        onGround = collidingFloor.Length > 0;
        animator.SetBool("InAir", !onGround);

        float horizontalMovement = -Input.GetAxis("Horizontal");
        

        Vector3 desiredMovement = new Vector3(horizontalMovement * speed * Time.deltaTime, 0, 0);
        transform.Translate(desiredMovement);

        
        if (onGround && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector2.up * jumpStrength, ForceMode.Impulse);

            if(jumpSfx != null)
            {
                jumpSfx.Play();
            }
        }
        LimitVelocity();
        RotationControl();
    }

    void LimitVelocity()
    {
        if(rb.velocity.magnitude >= maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
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
