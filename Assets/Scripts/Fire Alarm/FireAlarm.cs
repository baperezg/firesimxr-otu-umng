using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using Unity.VisualScripting;

public class FireAlarm : MonoBehaviour
{
    [Header("Activation Objects")]
    [SerializeField] private GameObject[] warningSignals;
    [SerializeField] private AudioSource talkingSounds;

    [Header("Task Ui")]
    public TextMeshProUGUI taskDone;
    public bool isCompleted = false;

    private void Start()
    {
        isCompleted = false;
    }

    private void Update()
    {
        if (this.transform.rotation.x > 0.0)
        {
            AlarmPulled();
        }
    }
    private void AlarmPulled()
    {
        talkingSounds.volume = Mathf.Lerp(talkingSounds.volume, 0.0f, Time.deltaTime / 1); ;
        foreach(var signal in warningSignals)
        {
            signal.SetActive(true);
        }
        taskDone.fontStyle = FontStyles.Strikethrough;
        isCompleted = true;
        Destroy(GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>());
    }
}
