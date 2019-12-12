using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBroken : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Player")
        {
            GameManager.Get().player.landOnWood = true;
            anim.SetTrigger("PlayerEnter");
        }
    }

    public void ExitWood()
    {
        GameManager.Get().player.landOnWood = false;
    }

    public void PlaySound()
    {
        AkSoundEngine.PostEvent("trap_wood_floor_break", gameObject);
    }

    private void OnDisable()
    {
        anim.SetTrigger("GoToIdle");
    }
}
