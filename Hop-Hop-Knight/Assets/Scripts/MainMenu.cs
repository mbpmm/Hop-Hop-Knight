using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Animator animator;
    public Text version;

    private int levelToLoad;

    public Toggle music;
    public Toggle fx;

    public AK.Wwise.State OnCreditsEnter;
    public AK.Wwise.State OnCreditsExit;
    public AK.Wwise.State OnSettingsEnter;
    public AK.Wwise.State OnSettingsExit;

    private void Start()
    {
        AkSoundEngine.PostEvent("game_start", gameObject);
        Time.timeScale = 1;
        version.text = "v" + Application.version;
        music.isOn = GameManager.Get().music;
        fx.isOn = GameManager.Get().fx;
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
        FadeToLevel(2);
    }

    public void Credits()
    {
        AkSoundEngine.PostEvent("ui_menu_enter", gameObject);
        OnCreditsEnter.SetValue();
    }

    public void CreditsExit()
    {
        AkSoundEngine.PostEvent("ui_menu_back", gameObject);
        OnCreditsExit.SetValue();
    }

    public void Settings()
    {
        AkSoundEngine.PostEvent("ui_menu_enter", gameObject);
        OnSettingsEnter.SetValue();
    }

    public void SettingsExit()
    {
        AkSoundEngine.PostEvent("ui_menu_back", gameObject);
        OnSettingsExit.SetValue();
    }

    public void QuitApp()
    {
        Application.Quit();
    }

    public void MusicToggle(bool newValue)
    {
        GameManager.Get().music = newValue;
    }

    public void FxToggle(bool newValue)
    {
        GameManager.Get().fx = newValue;
    }

}
