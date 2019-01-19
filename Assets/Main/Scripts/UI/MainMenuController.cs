using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour
{
    public GameObject main, controls;

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }

    public void ShowControls()
    {
        main.SetActive(false);
        controls.SetActive(true);
    }

    public void ShowMainMenu()
    {
        main.SetActive(true);
        controls.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
