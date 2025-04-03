using UnityEngine.InputSystem;
using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public Transform head;
    public float spawnDistance = 2f;

    public GameObject menu;
    public GameObject hud;
    public GameObject aboutUI;

    public InputActionProperty pauseButton;
    public InputActionProperty timerButton;

    private bool pauseUIActive = true;
    private Animator aboutAnimator;

    void Start()
    {
        if (aboutUI != null)
        {
            PositionUI(aboutUI);
            aboutAnimator = aboutUI.GetComponent<Animator>();
            aboutUI.SetActive(true);
            aboutAnimator.SetTrigger("Open");
        }

        DisplayPauseScreen();
    }

    void Update()
    {
        if (pauseButton.action.WasPerformedThisFrame())
        {
            DisplayPauseScreen();
        }
        else if (timerButton.action.WasPerformedThisFrame())
        {
            hud.SetActive(!hud.activeSelf);
            PositionUI(hud);
        }

        RotateUIToFace(menu);
        RotateUIToFace(hud);
    }

    public void DisplayPauseScreen()
    {
        if (pauseUIActive)
        {
            menu.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            PositionUI(menu);
            menu.SetActive(true);
            Time.timeScale = 0;
        }

        pauseUIActive = !pauseUIActive;
    }

    public void HideAboutUI()
    {
        if (aboutAnimator != null)
        {
            aboutAnimator.SetTrigger("Close");
            StartCoroutine(DeactivateAfterAnimation(aboutUI, 0.3f)); // Delay matches close anim time
        }
    }

    private IEnumerator DeactivateAfterAnimation(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        obj.SetActive(false);
    }

    private void PositionUI(GameObject ui)
    {
        if (ui == null) return;
        Vector3 spawnPos = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;
        ui.transform.position = spawnPos;
        RotateUIToFace(ui);
    }

    private void RotateUIToFace(GameObject ui)
    {
        if (ui == null) return;
        Vector3 lookDir = ui.transform.position - head.position;
        lookDir.y = 0;
        if (lookDir.sqrMagnitude > 0.001f)
            ui.transform.rotation = Quaternion.LookRotation(lookDir, Vector3.up);
    }
}
