using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public bool gameIsPaused = false;

    [Header("UI References")]
    [SerializeField] public GameObject pauseMenuUI;
    [SerializeField] public GameObject HudOverlayUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (gameIsPaused)
            {
                Resume();
            } else {
                Pause();
            }
        }  
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
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
}
