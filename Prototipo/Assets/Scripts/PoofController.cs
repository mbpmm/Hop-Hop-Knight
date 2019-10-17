using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoofController : MonoBehaviour
{
    public Rigidbody2D playerRB;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponentInParent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerRB.velocity==Vector2.zero)
        {
            anim.SetBool("Poof",true);
        }
        else
        {
            anim.SetBool("Poof", false);
        }
    }
}
