using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Darkness : MonoBehaviour
{
    public float speed;
    public GameObject player;
    public Transform startPos;
    public float distance;
    public float aux1;
    public float aux2;
    private float maxValue=7.55f;
    public float percentage;
    // Start is called before the first frame update
    void Start()
    {
        aux1 = -14.1631f;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (GameManager.Get().score != 0)
        {
            distance = Vector2.Distance(player.transform.position, transform.position);
            aux2 = Mathf.Abs(aux1 + distance);
            percentage = (aux2 * 100f) / maxValue;
            transform.position += transform.up * Time.deltaTime * speed;
            AkSoundEngine.SetRTPCValue("distance_trap_tentacle", percentage);
        }

        if (transform.position.y < startPos.position.y)
        {
            transform.position = startPos.position;
        }
    }
}
