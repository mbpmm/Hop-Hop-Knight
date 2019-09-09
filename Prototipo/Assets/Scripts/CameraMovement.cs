using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public bool follow;
    public Transform target;
    public Vector3 offset;
    public Vector3 rotation;
    private Vector3 initialPos;
    
    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
        follow = true;
    }

    void LateUpdate()
    {
        if (follow)
        {
            transform.position = new Vector3(transform.position.x, target.position.y, target.position.z) + offset;
            transform.eulerAngles = rotation;
        }

    }
    private void OnMouseDown()
    {
        follow = false;
    }

    private void OnMouseUp()
    {
        follow = true;
    }
    // Update is called once per frame
    
}