using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    private Rigidbody2D playerRB;
    public Vector3 offset;
    public Vector3 desiredPos;
    public AnimationCurve animCurve;
    public float speed;
    public float totalTime;
    
    void Start()
    {
        playerRB = target.gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (playerRB.velocity==Vector2.zero)
        {
            desiredPos= new Vector3(transform.position.x, target.position.y, target.position.z) + offset;
        }

        Advance();
    }

    public void Advance()
    {
        StartCoroutine(Animate());
    }
    
    IEnumerator Animate()
    {
        float t = 0;
        if (t<=totalTime)
        {
            t += Time.deltaTime*speed;
            transform.position = Vector3.Lerp(transform.position, desiredPos, animCurve.Evaluate(t / totalTime));
            yield return null;
        }
    }
}