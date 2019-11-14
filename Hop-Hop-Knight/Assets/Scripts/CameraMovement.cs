using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    private Player2 player;
    public Transform darknessStart;
    public Vector3 offset;
    public Vector3 desiredPos;
    public AnimationCurve animCurve;
    public float speed;
    public float totalTime;
    public float currentAspect;

    void Start()
    {
        player = target.gameObject.GetComponent<Player2>();

        currentAspect= (float)Screen.height / (float)Screen.width;

        if (currentAspect > 1.9f)
        {
            Camera.main.orthographicSize = 13.85f;
            //offset = new Vector3(0, 15f, -80f);
            darknessStart.position=new Vector3(0, -20.3f, 33f);
        }
        else
        {
            Camera.main.orthographicSize = 12.03f;
           // offset = new Vector3(0, 13f, -80f);
            darknessStart.position = new Vector3(0, -18.55f, 33f);
        }
        //desiredPos = new Vector3(transform.position.x, target.position.y, target.position.z) + offset;
        Advance();
    }
    void Update()
    {
        if (player.isGrounded)
        {
            desiredPos = new Vector3(transform.position.x, target.position.y, target.position.z) + offset;
        }

        if (!player.isDead )
        {
        }
        else
        {

        }
    }

    public void Advance()
    {
        StartCoroutine(Animate(totalTime));
    }
    public float delta;
    //x = x0 + (x1-x0) * eval;
    IEnumerator Animate(float time)
    {
        float t = 0;
        float initPosY = transform.position.y;
        delta = (desiredPos.y - initPosY);
        while (t<time)
        {
            t += Time.deltaTime*speed;
            if (t>time)
            {
                t = time;
            }
            float y = initPosY + (delta * animCurve.Evaluate(t / time));
            transform.position = new Vector3(transform.position.x,y ,transform.position.z);
            //transform.position = Vector3.LerpUnclamped(transform.position, desiredPos, animCurve.Evaluate(t / time));
            yield return null;
        }
    }
}