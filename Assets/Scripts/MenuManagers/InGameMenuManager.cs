using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameMenuManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject configMenu;

    void Start()
    {
        pauseMenu.SetActive(false);
        configMenu.SetActive(false);
    }

    public void PauseGame()
    {
        if(Time.timeScale != 0)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            configMenu.SetActive(false);
        }
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Config()
    {
        if (pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(false);
            configMenu.SetActive(true);
        }
        else
        {
            pauseMenu.SetActive(true);
            configMenu.SetActive(false);
        }

    }
}
