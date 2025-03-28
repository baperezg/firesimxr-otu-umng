using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public Transform head;
    public float spawnDistance; 
    
    public GameObject menu;
    public GameObject hud;

    public InputActionProperty pauseButton;
    public InputActionProperty timerButton;

    public bool pauseUIActive = true;
    
    // Start is called before the first frame update
    void Start()
    {
        DisplayPauseScreen();
    }

    private void Update()
    {
        if (pauseButton.action.WasPerformedThisFrame())
        {
            DisplayPauseScreen();
        }
        else if (timerButton.action.WasPerformedThisFrame())
        {
            hud.SetActive(!hud.activeSelf);
            hud.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;
        }

        // Rotate menu toward head
        Vector3 menuDir = head.position - menu.transform.position;
        menuDir.y = 0;
        if (menuDir.sqrMagnitude > 0.001f)
            menu.transform.rotation = Quaternion.LookRotation(menuDir);

        // Rotate HUD toward head
        Vector3 hudDir = head.position - hud.transform.position;
        hudDir.y = 0;
        if (hudDir.sqrMagnitude > 0.001f)
            hud.transform.rotation = Quaternion.LookRotation(hudDir);
    }
    public void DisplayPauseScreen()
    {
        if (pauseUIActive)
        {
            menu.SetActive(false);
            Time.timeScale = 1;

            pauseUIActive = false;

           
        }

        else if(!pauseUIActive)
        {
            menu.SetActive(true);
            pauseUIActive = true;
            Time.timeScale = 0;


            menu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;

            
        }
    }

    
}
