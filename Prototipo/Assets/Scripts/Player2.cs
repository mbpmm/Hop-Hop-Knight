﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Player2 : MonoBehaviour
{

    public delegate void OnPlayerAction();
    public static OnPlayerAction platformTouch;
    public static OnPlayerAction playerDeath;
    Rigidbody2D rbody;
    Vector2 startpos;
    Vector2 endpos;
    Vector2 mousePos;
    public Vector2 direction;
    public float power = 5f; // power of shot
    public float distance;
    public float timeStart;
    public float timeEnd;
    public float timeInterval;

    public bool isGrounded;
    public bool isDead;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    public bool launched;
    public bool hideMira;

    public GameObject mira;
    public GameObject miraVisual;
    public Vector3 scale;
    public float miraSize = 1;
    public float miraDivider;

    public float angle;

    private Animator animator;
    public int dir;
    public float timeIdle;
    public bool idleBlink;

    public float dirLimitY;
    public float dirLimitX;
    public GameObject activeParent;

    public SpriteRenderer spriteRenderer;
    public Sprite jumpSprite;

    //POWER UP
    public int cantGemas=0;
    public int totalGemas=5;
    public bool powerUpActivated;

    //CAMERA SHAKE
    public GameObject cam;
    private CameraMovement camShake;
    void Start()
    {
        GameManager.Get().player = this;
        rbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        camShake = cam.GetComponent<CameraMovement>();
    }

    void MiraUpdate()
    {
        direction = startpos - endpos;

        LimitMira();

        miraSize = direction.magnitude * miraDivider;

        scale = miraVisual.transform.localScale;

        scale.y = miraSize;

        miraVisual.transform.localScale = scale;
        miraVisual.transform.localPosition = Vector3.zero + Vector3.up * miraSize;

        angle = Vector3.Angle(direction, Vector3.up);

        if (startpos.x > endpos.x)
        {
            angle = -angle;
        }

        if (angle > 90)
        {
            angle = 90;
        }
        else if (angle < -90)
        {
            angle = -90;
        }
        mira.transform.rotation = Quaternion.Euler(0, 0, angle);
        mira.transform.position = transform.position;

        //Debug.Log(miraSize);
        if (miraVisual.transform.localScale.y > 1.1f)
        {
            scale.y = 1.1f;
            miraVisual.transform.localScale = scale;
            miraVisual.transform.localPosition = Vector3.zero + Vector3.up * 1.1f;
        }

        if (direction.y < 0f || hideMira)
        {
            miraVisual.gameObject.SetActive(false);
        }
        else
        {
            Invoke("ActivateMira", 0.1f);
        }
    }

    void Update()
    {
        MiraUpdate();
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        if (isGrounded)
        {
            launched = false;
        }

        if (Input.GetMouseButtonDown(0) && isGrounded && !isDead)
        {
            hideMira = false;
            Invoke("ActivateMira", 0.1f);
            animator.SetBool("PreJump", true);
            spriteRenderer.sprite = jumpSprite;
            startpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            timeStart = Time.time;
        }

        if (Input.GetMouseButton(0))
        {
            endpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0) && !launched && !isDead)
        {
            hideMira = true;
            animator.SetBool("PreJump", false);
            miraVisual.gameObject.SetActive(false);
            launched = true;
            endpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            timeEnd = Time.time;
            timeInterval = timeEnd - timeStart;
            LaunchPlayer();
        }

        if (Input.GetMouseButtonUp(0) && rbody.velocity == Vector2.zero && Time.timeScale != 0)
        {
            animator.SetTrigger("GoToIdle");
        }

        if (rbody.velocity == Vector2.zero)
        {
            dir = 0;
            timeIdle += Time.deltaTime;
            animator.SetBool("IsJumping", false);
            animator.SetInteger("Direction", dir);
            animator.SetFloat("TimeIdle", timeIdle);
            animator.SetBool("IdleBlink", idleBlink);
            if (timeIdle > 5.8f)
            {
                idleBlink = true;

            }
            if (timeIdle > 9f)
            {
                idleBlink = false;
                timeIdle = 0;
            }
        }
        else
        {
            launched = true;
            animator.SetBool("IsJumping", true);
        }

        if (cantGemas==totalGemas)
        {
            powerUpActivated = true;
        }
    }

    //void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawSphere(groundCheck.position, checkRadius);
    //}

    private void OnMouseDown()
    {
        spriteRenderer.sprite = jumpSprite;
    }

    void LaunchPlayer()
    {
        direction = startpos - endpos;

        LimitMira();

        rbody.velocity = power * direction; // swap subtraction to switch direction of launch
        //rbody.AddForce(direction *  power);
    }

    public void LimitMira()
    {
        if (direction.y > dirLimitY)
        {
            direction = new Vector2(direction.x, dirLimitY);
        }

        if (direction.y < 0f)
        {
            direction = new Vector2(0f, 0f);
        }

        if (direction.x < -dirLimitX)
        {
            direction = new Vector2(-dirLimitX, direction.y);
        }

        if (direction.x > dirLimitX)
        {
            direction = new Vector2(dirLimitX, direction.y);
        }
    }

    void ActivateMira()
    {
        miraVisual.gameObject.SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemies")
        {
            if (!powerUpActivated)
            {
                isDead = true;
                if (playerDeath != null)
                    playerDeath();
            }
            
        }

        if (collision.gameObject.tag == "MovingFloor")
        {
            if (activeParent == null || activeParent.gameObject.tag == "MovingFloor")
            {
                this.transform.parent = collision.transform;
                activeParent = collision.gameObject;
            }

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MovingFloor")
        {
            if (activeParent == collision.gameObject)
            {
                this.transform.parent = null;
                activeParent = null;
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Score")
        {
            if (platformTouch != null)
                platformTouch();
            collision.gameObject.SetActive(false);

        }
        if (collision.gameObject.tag == "Enemies")
        {
            if (!powerUpActivated)
            {
                isDead = true;
                if (playerDeath != null)
                    playerDeath();
            }
        }

        if (collision.gameObject.tag == "Gem")
        {
            cantGemas++;
            Destroy(collision.gameObject);
        }
    }


}
