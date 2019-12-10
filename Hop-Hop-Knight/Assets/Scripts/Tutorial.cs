using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject blockDestructionPoint;

    private void Start()
    {
        blockDestructionPoint = GameObject.Find("BlockDestructionPoint");
    }
    private void Update()
    {
        if (transform.position.y < blockDestructionPoint.transform.position.y)
        {
            gameObject.SetActive(false);
        }
    }
    public void TutorialSound()
    {
        AkSoundEngine.PostEvent("hud_hand", gameObject);
    }
}
