using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fantasma : MonoBehaviour
{
    public bool follow;
    public float timer;
    public float timeFollowing;
    public float speed;
    public GameObject player;
    public float distance;
    public bool movingRight = true;
    public Transform groundDetection;
    private Animator anim;
    public Transform initPos;

    public float distancePlayer;
    public float aux1;
    public float aux2;
    private float maxValue = 16f;
    public float percentage;

    // Start is called before the first frame update
    void Start()
    {
        aux1 = -15f;
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (follow && timer<timeFollowing)
        {
            anim.SetBool("Attacking", true);
            timer += Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            if (player.transform.position.x > transform.position.x)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else if (player.transform.position.x < transform.position.x)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
            }
        }
        else
        {
            anim.SetBool("Attacking", false);
            transform.Translate(Vector2.right * Time.deltaTime * speed);

            RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
            if (groundInfo.collider == false || groundInfo.collider.tag == "Wall")
            {
                if (movingRight == true)
                {
                    transform.eulerAngles = new Vector3(0, -180, 0);
                    movingRight = false;
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    movingRight = true;
                }
            }
        }

        distancePlayer = Vector2.Distance(player.transform.position, transform.position);

        if (distancePlayer < 15f)
        {
            aux2 = Mathf.Abs(aux1 + distancePlayer);
            percentage = (aux2 * 100f) / maxValue;
            Mathf.Clamp(percentage, 0, 100);
            AkSoundEngine.SetRTPCValue("distance_enemy_ghost", percentage);

        }
        else
        {
            percentage = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Player")
        {
            follow = true;
        }
        
    }

    public void Attack()
    {
        anim.SetTrigger("Attackloop");
    }

    private void OnEnable()
    {
        transform.position = initPos.position;
        timer = 0;
        follow = false;
    }
}
