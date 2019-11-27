using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Animator animator;
    public Text version;

    private int levelToLoad;

    private void Start()
    {
        Time.timeScale = 1;
        version.text = "v" + Application.version;
    }
    void Update()
    {

    }

    public void FadeToLevel(int levelIndex)
    {
        levelToLoad = levelIndex;
        animator.SetTrigger("IsClicked");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void GoToGame()
    {
        AkSoundEngine.PostEvent("ui_menu_start", gameObject);
        FadeToLevel(1);
    }

    public void Credits()
    {
        AkSoundEngine.PostEvent("ui_menu_credits", gameObject);
    }

    public void QuitApp()
    {
        Application.Quit();
    }

}
