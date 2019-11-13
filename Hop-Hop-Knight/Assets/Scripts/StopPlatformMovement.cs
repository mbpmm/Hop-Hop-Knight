using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopPlatformMovement : MonoBehaviour
{
    public delegate void OnPlayerEnter();
    public OnPlayerEnter movingPlatformTouch;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="Player")
        {
            if (movingPlatformTouch != null)
                movingPlatformTouch();
        }
    }
}
