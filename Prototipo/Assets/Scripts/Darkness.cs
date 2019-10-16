using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Darkness : MonoBehaviour
{
    public float speed;
    public GameObject player;

    public Rigidbody2D playerRB;
    public Transform startPos;

    public GameObject gameManager;
    private GameManager gameMan;

    private CameraMovement camMov;

    public AnimationCurve animCurve;
    public float speed2;
    public float totalTime;
    // Start is called before the first frame update
    void Start()
    {
        playerRB = player.GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("GameManager");
        gameMan = gameManager.GetComponent<GameManager>();
        camMov = GetComponentInParent<CameraMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        // transform.position = player.transform.position;
        if (playerRB.velocity.y != 0)
        {
            Advance();
        }
        else
        {
            
        }

        if (gameMan.score != 0)
        {
            transform.position += transform.up * Time.deltaTime * speed;
        }
        //if (camMov.isMoving)
        //{
        //    transform.position -= transform.up * Time.deltaTime * speed * playerRB.velocity.y;
        //}

        if (transform.position.y < startPos.position.y)
        {
            transform.position = startPos.position;
        }

    }

    public void Advance()
    {
        StartCoroutine(Animate());
    }

    IEnumerator Animate()
    {

        float t = 0;
        if (t <= totalTime)
        {
            t += Time.deltaTime * speed2;
            transform.position = Vector3.Lerp(transform.position, startPos.position, animCurve.Evaluate(t / totalTime));
            yield return null;
        }
    }

    //public Transform target;
    //private Rigidbody2D playerRB;
    //public Vector3 offset;
    //public Vector3 desiredPos;
    //public AnimationCurve animCurve;
    //public float speed;
    //public float speed2;
    //public float totalTime;

    //void Start()
    //{
    //    playerRB = target.gameObject.GetComponent<Rigidbody2D>();
    //}

    //void Update()
    //{
    //    if (playerRB.velocity == Vector2.zero)
    //    {

    //        transform.position += transform.up * Time.deltaTime * speed2;
    //    }
    //    else
    //    {
    //        desiredPos = new Vector3(transform.position.x, target.position.y, target.position.z) + offset;
    //        transform.position += transform.up * Time.deltaTime * speed;
    //    }

    //    Advance();
    //}

    //public void Advance()
    //{
    //    StartCoroutine(Animate());
    //}

    //IEnumerator Animate()
    //{
    //    float t = 0;
    //    if (t <= totalTime)
    //    {
    //        t += Time.deltaTime * speed;
    //        transform.position = Vector3.Lerp(transform.position, desiredPos, animCurve.Evaluate(totalTime/t));
    //        yield return null;
    //    }
    //}
}
