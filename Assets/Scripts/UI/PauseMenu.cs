using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenu;
    public bool pauseUIActive = true;
    public GameObject leftRay, rigthRay;
    // Start is called before the first frame update
    void Start()
    {
        DisplayPauseScreen();
    }

    public void PauseButtonPress(InputAction.CallbackContext context)
    {
        if (context.performed)
            DisplayPauseScreen();

        
    }
    public void DisplayPauseScreen()
    {
        if (pauseUIActive)
        {
            pauseMenu.SetActive(false);
            pauseUIActive = false;
            Time.timeScale = 1;
            leftRay.SetActive(false);
            rigthRay.SetActive(false);
        }

        else if(!pauseUIActive)
        {
            pauseMenu.SetActive(true);
            pauseUIActive = true;
            Time.timeScale = 0;
            leftRay.SetActive(true);
            rigthRay.SetActive(true);
        }
    }
}
