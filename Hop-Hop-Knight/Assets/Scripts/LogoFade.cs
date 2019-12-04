using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogoFade : MonoBehaviour
{
    private void Start()
    {
        AkSoundEngine.PostEvent("developer_logo", gameObject);
    }

    public void GoToIntroScene()
    {
        SceneManager.LoadScene("IntroScene");
    }

}
