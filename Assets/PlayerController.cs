using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour

{
    public Rigidbody2D RB;
    [SerializeField] float MoveSpeed;
    [SerializeField] float JumpPower;
    [SerializeField] float Horizontal = (Input.GetAxisRaw("Horizontal"));
    [SerializeField] float StartingGravity;
    public bool Jumping = false;
    public bool Grounded = true;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask WhatIsGround;
    [SerializeField] float CoyoteTime;
    [SerializeField] float CoyoteTimeCounter;
    public bool IsFacingRight;


    private CameraFollowScript Camera;

    private float JumpTimeCounter;
    public float JumpTime;

    

    // Start is called before the first frame update
    void Start()
    {
        RB.gravityScale = StartingGravity;

        Camera = GameObject.FindGameObjectWithTag("Camera").GetComponent<CameraFollowScript>();

        IsFacingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 HorizontalInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);
        Vector3 HorizontalMovement = HorizontalInput * MoveSpeed * Time.deltaTime;
        transform.position = transform.position + HorizontalMovement;

        Grounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, WhatIsGround);

        if ((Input.GetAxisRaw("Horizontal") > 0) && !IsFacingRight)
            {
                Turn();
               
            }

        else if ((Input.GetAxisRaw("Horizontal")) < 0 && IsFacingRight)
        {
            Turn();
            
        }

        if (Grounded)
        {
            CoyoteTimeCounter = CoyoteTime;
        }
        else
        {
            CoyoteTimeCounter -= Time.deltaTime;
        }
       

        if (CoyoteTimeCounter > 0 && Input.GetKeyDown(KeyCode.Space))
        {
            Jumping = true;
            JumpTimeCounter = JumpTime;
            RB.velocity = Vector2.up * JumpPower;
            
        }

        if (Input.GetKey(KeyCode.Space) && Jumping == true)
        {
            if (JumpTimeCounter > 0)
            {
                RB.velocity = Vector2.up * JumpPower;
                JumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                Jumping = false;
            }
         
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Jumping = false;
            CoyoteTimeCounter = 0;
        }
      



    }

    void Turn()
    {
        if (IsFacingRight)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 180, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            IsFacingRight = false;
            Camera.CallTurn();
        }
        else
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 0, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            IsFacingRight = true;
            Camera.CallTurn();
        }
        
    }
}