using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void PracticeGame()
    {
        SceneManager.LoadScene("PracticeScene");
    }
    public void MainMenu()
    {
        ResetGame();
        SceneManager.LoadScene("MainMenu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    private void ResetGame()
    {
        Time.timeScale = 1;
    }
}
