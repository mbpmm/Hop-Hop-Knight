using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningSound : MonoBehaviour
{
    public void WarningSoundPost()
    {
        AkSoundEngine.PostEvent("ui_ingame_warning", gameObject);
    }
}
