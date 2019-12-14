using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class Player : MonoBehaviour
{

    public delegate void OnPlayerAction();
    public static OnPlayerAction platformTouch;
    public static OnPlayerAction playerDeath;
    public static OnPlayerAction powerUpScore;
    public static Action<Player> playerStarted;
    Rigidbody2D rbody;
    BoxCollider2D boxCol;
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

    //MIRA
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

    //POOF
    public ParticleSystem poofPS;
    public bool doPoof;
    private Vector2 velocityFrameAnt;

    public float dirLimitY;
    public float dirLimitX;
    public GameObject activeParent;

    //POWER UP
    public int cantGemas=0;
    public int totalGemas=5;
    public bool powerUpActivated;

    public Animator deathAnim;
    public float timerPU;
    public float totalTimePU;
    public Vector3 desiredPos;
    public bool goToIdle;

    public AK.Wwise.State Playing;
    public AK.Wwise.State PlayerAlive;
    public AK.Wwise.State PlayerDead;
    public AK.Wwise.State PlayerPowerUpEnter;
    public AK.Wwise.State PlayerPowerUpExit;
    public AK.Wwise.State Score;

    public bool onPause;
    public bool landAfterPU;
    public float speedOnX;
    public float speedPU;
    public float maxValueWall;
    public float minValueWall;

    public float totalTimeIdle;
    public float startBlinkIdle;

    public bool landOnWood;
    void Start()
    {
        playerStarted.Invoke(this);
        rbody = GetComponent<Rigidbody2D>();
        boxCol = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        PlayerAlive.SetValue();
        Playing.SetValue();
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

        if (Input.GetMouseButtonDown(0) && isGrounded && !isDead && !powerUpActivated )
        {
            if (Time.timeScale != 0 || !onPause)
            {
                AkSoundEngine.PostEvent("player_touch", gameObject);
            }
            
            hideMira = false;
            Invoke("ActivateMira", 0.1f);
            animator.SetTrigger("PreJ");
            startpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            timeStart = Time.time;
        }

        if (Input.GetMouseButton(0) && !isDead)
        {
            endpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0) && !launched && !isDead )
        {
            if (Time.timeScale!=0 || !onPause)
            {
                AkSoundEngine.PostEvent("player_release", gameObject);
            }
            
            hideMira = true;
            miraVisual.gameObject.SetActive(false);
            launched = true;
            endpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            timeEnd = Time.time;
            timeInterval = timeEnd - timeStart;
            LaunchPlayer();
        }

        if (Input.GetMouseButtonUp(0) && rbody.velocity == Vector2.zero && Time.timeScale != 0 && !powerUpActivated)
        {
            animator.SetTrigger("GoToIdle");
        }

        if (velocityFrameAnt!=Vector2.zero && rbody.velocity==Vector2.zero)
        {
            if (landOnWood)
            {
                AkSoundEngine.SetSwitch("floor_material", "wood", gameObject);
            }
            else
            {
                AkSoundEngine.SetSwitch("floor_material", "rock", gameObject);
            }

            AkSoundEngine.PostEvent("player_land", gameObject);

            poofPS.Play();
        }

        if (rbody.velocity == Vector2.zero)
        {
            timeIdle += Time.deltaTime;
            animator.SetBool("IsJumping", false);
            animator.SetFloat("TimeIdle", timeIdle);
            animator.SetBool("IdleBlink", idleBlink);
            if (timeIdle > totalTimeIdle)
            {
                idleBlink = true;

            }
            if (timeIdle > startBlinkIdle)
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
        
        if (cantGemas==totalGemas&&!powerUpActivated)
        {
            PlayerPowerUpEnter.SetValue();
            powerUpActivated = true;
            landAfterPU = true;
            AkSoundEngine.PostEvent("powerup_start", gameObject);
            animator.SetTrigger("PowerUp");
        }

        if (isDead && powerUpActivated)
        {
            animator.SetBool("PreJump", false);
        }

        if (powerUpActivated)
        {
            if (Input.touchCount > 0) 
            {

                Touch touchDeltaPosition = Input.GetTouch(0);
                Vector3 touchpos = Camera.main.ScreenToWorldPoint(touchDeltaPosition.position);
                touchpos.z = transform.position.z;

                Vector3 direction = (touchpos - transform.position);

                transform.Translate(direction.x * speedOnX,0, 0);
            }
            Mathf.Clamp(transform.position.x, minValueWall, maxValueWall);
            timerPU += Time.deltaTime;
            transform.Translate(0, speedPU*Time.deltaTime, 0);
            rbody.velocity = Vector2.zero;
            boxCol.isTrigger = true;
            if (timerPU > totalTimePU)
            {
                Invoke("DeactivatePU", 2f);
                Invoke("ActivateLanding", 2.5f);
                timerPU = 0;
                cantGemas = 0;
                goToIdle = true;
            }
        }
        else if (goToIdle)
        {
            animator.SetTrigger("GoToIdle");
            boxCol.isTrigger = false;
            goToIdle = false;
        }

        if (isDead)
        {
            hideMira=true;
        }
        
        velocityFrameAnt = rbody.velocity;
    }

    public void DeactivatePU()
    {
        PlayerPowerUpExit.SetValue();
        AkSoundEngine.PostEvent("powerup_end", gameObject);
        powerUpActivated = false;
        timerPU = 0;
    }

    public void ActivateLanding()
    {
        landAfterPU = false;
    }

    void LaunchPlayer()
    {
        direction = startpos - endpos;

        LimitMira();

        rbody.velocity = power * direction; 
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
        if (collision.gameObject.tag == "Enemies" || collision.gameObject.tag == "Blobert"|| collision.gameObject.tag == "Murcy" || collision.gameObject.tag == "Murcy2")
        {
            if (!powerUpActivated)
            {
                AkSoundEngine.PostEvent("player_die", gameObject);
                rbody.mass = 0.01f;
                isDead = true;
                animator.SetTrigger("IsDead");
                rbody.velocity = Vector2.zero;
                boxCol.enabled = false;
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
            if (!powerUpActivated)
            {
                if (platformTouch != null)
                    platformTouch();
                collision.gameObject.SetActive(false);
            }
            

        }
        if (collision.gameObject.tag == "Enemies" || collision.gameObject.tag == "Blobert" || collision.gameObject.tag == "Murcy" || collision.gameObject.tag == "Murcy2" || collision.gameObject.tag == "Bullet")
        {
            if (!powerUpActivated)
            {
                AkSoundEngine.PostEvent("player_die", gameObject);
                rbody.mass = 0.01f;
                isDead = true;
                PlayerDead.SetValue();
                animator.SetTrigger("IsDead");
                rbody.velocity = Vector2.zero;
                boxCol.enabled = false;
                if (playerDeath != null)
                    playerDeath();
            }
        }

        if (collision.gameObject.tag == "Gem")
        {
            if (!powerUpActivated)
            {
                AkSoundEngine.PostEvent("player_grab_gem", gameObject);
                cantGemas++;
            }
            else
            {
                AkSoundEngine.PostEvent("player_grab_gem", gameObject);
                powerUpScore();
            }
        }
    }

    public void WingsSound()
    {
        AkSoundEngine.PostEvent("player_wings", gameObject);
    }
    public void PlayDeathScreen()
    {
        Score.SetValue();
        AkSoundEngine.PostEvent("ui_ingame_score", gameObject);
        deathAnim.SetTrigger("OpenDeathScreen");
    }


}
