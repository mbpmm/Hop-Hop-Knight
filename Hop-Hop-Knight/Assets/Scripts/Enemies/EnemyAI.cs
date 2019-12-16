using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed;
    public float distance;
    public bool movingRight = true;
    public Transform groundDetection;
    private Rigidbody2D enemyRB;

    private GameObject player;

    public float distancePlayer;
    public float aux1;
    public float aux2;
    private float maxValue = 16f;
    public float percentage;
    // Start is called before the first frame update
    void Start()
    {
        aux1 = -15f;
        enemyRB = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector2.right * Time.deltaTime * speed);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
        if (groundInfo.collider==false || groundInfo.collider.tag=="Wall")
        {
            if (movingRight==true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }


        distancePlayer = Vector2.Distance(player.transform.position, transform.position);

        if (distancePlayer<15f)
        {
            aux2 = Mathf.Abs(aux1 + distancePlayer);
            percentage = (aux2 * 100f) / maxValue;
            Mathf.Clamp(percentage, 0, 100);

            if (gameObject.tag == "Murcy")
            {
                AkSoundEngine.SetRTPCValue("distance_enemy_bat1", percentage, this.gameObject);
            }
            if (gameObject.tag == "Murcy2")
            {
                AkSoundEngine.SetRTPCValue("distance_enemy_bat2", percentage, this.gameObject);
            }
            if (gameObject.tag == "Blobert")
            {
                AkSoundEngine.SetRTPCValue("distance_enemy_blob", percentage, this.gameObject);
            }

        }
        else
        {
            percentage = 0;
        }
        

    }

    private void OnDisable()
    {
        percentage = 0;
        if (gameObject.tag == "Murcy")
        {
            AkSoundEngine.PostEvent("enemy_bat1_stop", this.gameObject);
        }
        if (gameObject.tag == "Murcy2")
        {
            AkSoundEngine.PostEvent("enemy_bat2_stop", this.gameObject);
        }
        if (gameObject.tag == "Blobert")
        {
            AkSoundEngine.PostEvent("enemy_blob_stop", this.gameObject);
        }
    }

}
