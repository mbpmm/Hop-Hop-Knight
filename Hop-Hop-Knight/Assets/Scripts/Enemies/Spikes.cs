using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public void PlaySound()
    {
        AkSoundEngine.PostEvent("trap_spikes", gameObject);
    }
}
