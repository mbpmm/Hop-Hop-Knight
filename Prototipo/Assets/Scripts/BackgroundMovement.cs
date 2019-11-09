using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    public float speed;
    private float timer;
    MeshRenderer backRenderer;
    void Start()
    {
        backRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        Vector2 offset = new Vector2(0, timer * speed);
        backRenderer.material.mainTextureOffset = offset;
    }
}
