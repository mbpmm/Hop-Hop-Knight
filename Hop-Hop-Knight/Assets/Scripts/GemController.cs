using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemController : MonoBehaviour
{
    public GameObject blockDestructionPoint;
    public GameObject positionUI;
    public bool collected;
    public bool collectedPU;
    public float speed;
    public float speedPU;
    public float timer;
    public float totalTime=1f;
    public AnimationCurve animCurve;
    public AnimationCurve animCurveScale;

    void Start()
    {
        blockDestructionPoint = GameObject.Find("BlockDestructionPoint");
        positionUI = GameObject.Find("PowerBar");
    }

    void Update()
    {
        if (transform.position.y < blockDestructionPoint.transform.position.y)
        {
            gameObject.SetActive(false);
        }

        if (collected)
        {
            timer += Time.deltaTime * speed;
            Vector3 screenPoint = positionUI.transform.position + new Vector3(180, -100, 0);
            Vector3 gemsPosition = Camera.main.ScreenToWorldPoint(screenPoint);
            transform.position = Vector3.Lerp(transform.position, gemsPosition, animCurve.Evaluate(timer/totalTime));
            transform.localScale= Vector3.Lerp(transform.localScale, new Vector3(0,0,0), animCurveScale.Evaluate(timer / totalTime));

            if (animCurve.Evaluate(timer / totalTime)==1f)
            {
                gameObject.SetActive(false);
                collected = false;
                timer = 0;
                transform.localScale = new Vector3(0.11f, 0.11f, 0.11f);
            }
        }

        if (collectedPU)
        {
            timer += Time.deltaTime * speedPU;
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0, 0, 0), animCurve.Evaluate(timer / totalTime));

            if (animCurve.Evaluate(timer / totalTime) == 1f)
            {
                gameObject.SetActive(false);
                collectedPU = false;
                timer = 0;
                transform.localScale = new Vector3(0.11f, 0.11f, 0.11f);
            }
        }

        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="Player")
        {
            if (!GameManager.Get().player.powerUpActivated)
            {
                Debug.Log("colision con gema trigger");
                collected = true;
            }
            else
            {
                collectedPU = true;
            }
            
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("colision con gema colision");
        }
    }
}
