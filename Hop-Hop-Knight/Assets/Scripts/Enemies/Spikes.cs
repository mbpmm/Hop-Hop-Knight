using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public GameObject player;

    public float distancePlayer;
    public float aux1;
    public float aux2;
    private float maxValue = 16f;
    public float percentage;

    private void Start()
    {
        aux1 = -15f;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        distancePlayer = Vector2.Distance(player.transform.position, transform.position);

        if (distancePlayer < 15f)
        {
            aux2 = Mathf.Abs(aux1 + distancePlayer);
            percentage = (aux2 * 100f) / maxValue;
            Mathf.Clamp(percentage, 0, 100);
            AkSoundEngine.SetRTPCValue("distance_trap_spikes", percentage);

        }
        else
        {
            percentage = 0;
        }
    }
    public void PlaySoundOut()
    {
        AkSoundEngine.PostEvent("trap_spikes_out", gameObject);
    }

    public void PlaySoundIn()
    {
        AkSoundEngine.PostEvent("trap_spikes_in", gameObject);
    }
}
