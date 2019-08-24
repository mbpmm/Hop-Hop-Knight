using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class player : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public float vertical;
    public float horizontal;
    public bool exitOnce;
    public Animator animator;
    public int direction;
    public bool onTheGround;
    public bool doubleJumpAllowed;

    private float powerUpDuration;
    private bool powerUp;
    private Rigidbody2D playerRB;
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        exitOnce = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        vertical = CrossPlatformInputManager.GetAxis("Vertical");
        horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
        Vector2 movement = new Vector2(horizontal*speed, playerRB.velocity.y);

        playerRB.velocity = movement;

        //if (CrossPlatformInputManager.GetButtonDown("Jump") )
        //{
        //    playerRB.AddForce(Vector2.up * jumpForce);
        //}

        //if (horizontal < 0)
        //{
        //    direction = 1;
        //    animator.SetInteger("Direction", direction);
        //    animator.SetBool("IsMoving", true);
        //}
        //if (horizontal > 0)
        //{
        //    direction = 2;
        //    animator.SetInteger("Direction", direction);
        //    animator.SetBool("IsMoving", true);
        //}
        
        //if (horizontal == 0 && vertical == 0)
        //{
        //    direction = 0;
        //    animator.SetInteger("Direction", direction);
        //    animator.SetBool("IsMoving", false);
        //}

        if (playerRB.velocity.y == 0)
            onTheGround = true;
        else
            onTheGround = false;

        if (onTheGround)
            doubleJumpAllowed = true;

        if (onTheGround && CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            playerRB.velocity = new Vector2(playerRB.velocity.x, 0f);
            playerRB.AddForce(Vector2.up * jumpForce);
        }
        else if (doubleJumpAllowed && CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            playerRB.velocity = new Vector2(playerRB.velocity.x, 0f);
            playerRB.AddForce(Vector2.up * jumpForce);
            doubleJumpAllowed = false;
        }

    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (exitOnce)
        {
        }
    }
}
