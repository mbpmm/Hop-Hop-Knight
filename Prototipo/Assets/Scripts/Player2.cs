using System.Collections;
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
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    public bool launched;

    public GameObject mira;
    public GameObject miraVisual;
    public Vector3 scale;
    public float miraSize=1;
    public float miraDivider;

    public float angle;

    public Animator animator;
    public int dir;
    public float timeIdle;
    public bool idleBlink;

    public float dirLimit;
    public TextMeshProUGUI scoreText;
    public int score;

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void MiraUpdate()
    {
        direction = startpos - endpos;

        miraSize = direction.magnitude*miraDivider;

        scale = miraVisual.transform.localScale;

        scale.y = miraSize;

        miraVisual.transform.localScale = scale;
        miraVisual.transform.localPosition = Vector3.zero + Vector3.up * miraSize;

        angle = Vector3.Angle(direction, Vector3.up);

        if (startpos.x>endpos.x)
        {
            angle = -angle;
        }


        mira.transform.rotation = Quaternion.Euler(0,0,angle);
        mira.transform.position = transform.position;

        //Debug.Log(miraSize);
        if (miraVisual.transform.localScale.y > 1.1f)
        {
            scale.y = 1.1f;
            miraVisual.transform.localScale = scale;
            miraVisual.transform.localPosition = Vector3.zero + Vector3.up * 1.1f;
        }
    }

    void Update()
    {
        MiraUpdate();
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        scoreText.text = score.ToString();

        if (isGrounded)
        {
            launched = false;
        }

        if (Input.GetMouseButtonDown(0)&&isGrounded)
        {
            Invoke("DisableMira", 0.1f);
            
            startpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            timeStart = Time.time;
            Debug.Log(startpos);
        }

        if (Input.GetMouseButton(0))
        {
            endpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0) && !launched)
        {
            miraVisual.gameObject.SetActive(false);
            launched = true;
            endpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            timeEnd = Time.time;
            timeInterval = timeEnd - timeStart;
            LaunchPlayer();
        }

        if (rbody.velocity == Vector2.zero)
        {
            dir = 0;
            timeIdle += Time.deltaTime;
            animator.SetBool("IsJumping", false);
            animator.SetInteger("Direction", dir);
            animator.SetFloat("TimeIdle", timeIdle);
            animator.SetBool("IdleBlink", idleBlink);
            if (timeIdle>4f)
            {
                idleBlink = true;

            }
            if (timeIdle>5.5f)
            {
                idleBlink = false;
                timeIdle = 0;
            }
        }
        else
        {
            animator.SetBool("IsJumping", true);
        }

        Debug.Log(direction);
    }

    void LaunchPlayer()
    {
        direction = startpos - endpos;

        if (direction.y > dirLimit )
        {
            direction = new Vector2(direction.x, dirLimit);
        }

        if (direction.y < 0f)
        {
            direction = new Vector2(direction.x, 0f);
        }

        if (direction.x < -dirLimit)
        {
            direction = new Vector2(-dirLimit, direction.y);
        }

        if (direction.x > dirLimit)
        {
            direction = new Vector2(dirLimit, direction.y);
        }
        rbody.velocity = power*direction; // swap subtraction to switch direction of launch
        //rbody.AddForce(direction *  power);
    }

    void DisableMira()
    {
        miraVisual.gameObject.SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Enemies")
        {
            if (playerDeath != null)
                playerDeath();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="Score")
        {
            if (platformTouch != null)
                platformTouch();
            collision.gameObject.SetActive(false);
        }
    }

    //public Vector2 startPos;
    //public Vector2 endPos;
    //public Vector2 direction;
    //public bool directionChosen;
    //public float distance;
    //public float touchTimeStart;
    //public float touchTimeFinish;
    //public float timeInterval;
    //Rigidbody2D rbody;

    //public float power = 0.3f; // power of shot
    //void Start()
    //{
    //    rbody = GetComponent<Rigidbody2D>();
    //}
    //void Update()
    //{
    //    // Track a single touch as a direction control.
    //    if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
    //    {
    //        touchTimeStart = Time.time;
    //        startPos = Input.GetTouch(0).position;
    //    }

    //    if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
    //    {
    //        touchTimeFinish = Time.time;

    //        timeInterval = touchTimeFinish - touchTimeStart;
    //        endPos = Input.GetTouch(0).position;
    //        direction = startPos - endPos;
    //        rbody.AddForce((direction / timeInterval * power)/17f);
    //    }
    //}
}
