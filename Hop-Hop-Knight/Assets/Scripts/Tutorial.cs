using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public void TutorialSound()
    {
        AkSoundEngine.PostEvent("hud_hand", gameObject);
    }
}
