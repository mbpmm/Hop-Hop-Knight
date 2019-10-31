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
            offset = new Vector3(0, 7.5f, -80f);
            darknessStart.position=new Vector3(0, -20.3f, 33f);
        }
        else
        {
            Camera.main.orthographicSize = 12.03f;
            offset = new Vector3(0, 5.52f, -80f);
            darknessStart.position = new Vector3(0, -18.55f, 33f);
        }
    }

    void Update()
    {
        if (player.isGrounded)
        {
            desiredPos = new Vector3(transform.position.x, target.position.y, target.position.z) + offset;
        }

        if (!player.isDead)
        {
            Advance();
        }
        else
        {

        }
        
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

    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.position;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.position = new Vector3(transform.position.x+x, transform.position.y+ y, originalPos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPos;
    }
}