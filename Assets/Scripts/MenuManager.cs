using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public bool gameIsPaused = false;

    [Header("UI References")]
    [SerializeField] public GameObject pauseMenuUI;
    [SerializeField] public GameObject settingsMenuUI;
    [SerializeField] public GameObject HudOverlayUI;

    [Header("Wwise")]
    [SerializeField] public AK.Wwise.Event MenuClick;

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(false);
        HudOverlayUI.SetActive(true);
        Time.timeScale = 1f;
        gameIsPaused = false;

    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        HudOverlayUI.SetActive(false);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }


    public void Settings()
    {
        settingsMenuUI.SetActive(!settingsMenuUI.activeSelf);
        pauseMenuUI.SetActive(!pauseMenuUI.activeSelf);
    }

    public void ReloadScene()
    {
        Resume();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }

    public void ButtonAudio()
    {
        MenuClick.Post(gameObject);
    }
}
