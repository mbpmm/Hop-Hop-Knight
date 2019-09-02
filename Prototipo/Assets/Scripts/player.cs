using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    public float attackTime;
    public float startAttackTime;
    public Transform attackPos;
    public LayerMask enemies;
    public float attackRange;
    public int damage;
    public Button attackBtn;
    public bool isAttacking;

    public bool isWallSliding;
    public Transform wallCheck;
    public float wallCheckDistance;
    public RaycastHit2D wallCheckHit;
    public LayerMask wall;
    public float maxWallSlidingVel;

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

        if (horizontal > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            direction = 1;
            animator.SetInteger("Direction", direction);
            animator.SetBool("IsMoving", true);
        }
        if (horizontal < 0)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
            direction = 2;
            animator.SetInteger("Direction", direction);
            animator.SetBool("IsMoving", true);
        }

        //if (horizontal == 0 && vertical == 0)
        //{
        //    direction = 0;
        //    animator.SetInteger("Direction", direction);
        //    animator.SetBool("IsMoving", false);
        //}

        if (playerRB.velocity.y < 0.005f&& playerRB.velocity.y >= -0.005f)
            onTheGround = true;
        else
            onTheGround = false;

        if (onTheGround)
            doubleJumpAllowed = true;

        if (onTheGround && CrossPlatformInputManager.GetButtonDown("Jump") || onTheGround && Input.GetKeyDown(KeyCode.Space))
        {
            //playerRB.velocity = new Vector2(playerRB.velocity.x, 0f);
            playerRB.AddForce(Vector2.up * jumpForce);
        }
        else if (doubleJumpAllowed && CrossPlatformInputManager.GetButtonDown("Jump") && onTheGround==false || doubleJumpAllowed && Input.GetKeyDown(KeyCode.Space) && onTheGround == false)
        {
            playerRB.velocity = new Vector2(playerRB.velocity.x, 0f);
            playerRB.AddForce(Vector2.up * jumpForce);
            doubleJumpAllowed = false;
        }

        wallCheckHit = Physics2D.Raycast(wallCheck.position, wallCheck.right, wallCheckDistance, wall);

        if (wallCheckHit && playerRB.velocity.y < -0.005f)
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }

        if (isWallSliding)
        {
            if (playerRB.velocity.y< -maxWallSlidingVel)
            {
                playerRB.velocity = new Vector2(playerRB.velocity.x, -maxWallSlidingVel);
            }
            
        }

        if (attackTime <= 0 && isAttacking)
        {
            attackTime = startAttackTime;
            isAttacking = false;
        }
        else
        {
            attackTime -= Time.deltaTime;
        }
    }

    public void Attack()
    {
        if (attackTime <= 0 )
        {
            isAttacking = true;
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemies);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<EnemyAI>().TakeDamage(damage);
            }
        }
       
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.tag == "Floor")
        //{
        //    playerRB.velocity = new Vector2(playerRB.velocity.x, 0);
        //}

        //if (collision.gameObject.tag == "Wall")
        //{
        //    isWallSliding = true;
        //}
        //else
        //{
        //    isWallSliding = false;
        //}
    }
}
