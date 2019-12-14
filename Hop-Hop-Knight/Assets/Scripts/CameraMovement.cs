using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    private Player player;
    public Transform darknessStart;
    public Vector3 offset;
    public Vector3 desiredPos;
    public AnimationCurve animCurve;
    public float speed;
    public float totalTime;
    public float currentAspect;
    public float initPosY;

    private float deltaMin = 1.5f;
    private float deltaCorrection = 8f;
    void Start()
    {
        player = target.gameObject.GetComponent<Player>();

        currentAspect= (float)Screen.height / (float)Screen.width;

        if (currentAspect > 1.9f)
        {
            Camera.main.orthographicSize = 13.85f;
            offset = new Vector3(0, 7.5f, -80f);
            darknessStart.position=new Vector3(0, -21.3f, 33f);
        }
        else
        {
            Camera.main.orthographicSize = 12.03f;
            offset = new Vector3(0, 5.5f, -80f);
            darknessStart.position = new Vector3(0, -19.55f, 33f);
        }
        desiredPos = new Vector3(transform.position.x, target.position.y, target.position.z) + offset;
        Advance();

        Player.platformTouch += Advance;
    }
    void FixedUpdate()
    {
        if (player.isGrounded)
        {
            desiredPos = new Vector3(transform.position.x, target.position.y, target.position.z) + offset;
        }


        if (player.powerUpActivated)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, target.position.z) + new Vector3(0,0,offset.z);
            transform.Translate(0, 14.8f * Time.deltaTime, 0);
        }

        if (player.isDead)
        {
            Player.platformTouch -= Advance;
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
        initPosY = transform.position.y;
        delta = (desiredPos.y - initPosY);
        if (delta < deltaMin && !player.landAfterPU)
        {
            delta = deltaCorrection;
        }
        while (t<time)
        {
            t += Time.deltaTime*speed;
            if (t>time)
            {
                t = time;
            }
            float y = initPosY + (delta * animCurve.Evaluate(t / time));
            transform.position = new Vector3(transform.position.x,y ,transform.position.z);
            yield return null;
        }
    }
}