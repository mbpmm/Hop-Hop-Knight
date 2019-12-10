using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningSound : MonoBehaviour
{
    public void WarningSoundPost()
    {
        AkSoundEngine.PostEvent("hud_warning", gameObject);
    }
}
