using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Movement;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Turning;


public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance { get; private set; }

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    [Header("Sliders")]
    public Slider audioSlider;
    public Slider sensSlider;
    public Slider moveSlider;

    [Header("Components")]
    public ContinuousMoveProvider moveProvider;  
    public ContinuousTurnProvider continuousTurn;
    public SnapTurnProvider snapTurn;

    [Header("Components")]
    private float volume;
    private float sensitivity;
    private float movementSpeed;
    private void Start()
    {
        LoadSettings();

        int turnTypeIndex = PlayerPrefs.GetInt("TurnType", 0);
        SetTypeFromIndex(turnTypeIndex);
    }
    public void ApplySettings()
    {
        SetVolume(audioSlider.value);
        SetTurnSpeed(sensSlider.value);
        SetMoveSpeed(moveSlider.value);

        PlayerPrefs.SetString("Settings", audioSlider.value + "," + sensSlider.value + "," + moveSlider.value);
        PlayerPrefs.Save();
    }
    public void LoadSettings()
    {
        ReadSettings(PlayerPrefs.GetString("Settings"));

        audioSlider.value = volume;
        sensSlider.value = sensitivity;
        moveSlider.value = movementSpeed;

        Debug.Log(PlayerPrefs.GetString("Settings"));
        ApplySettings();
    }

    public void SetVolume(float masterVolume)
    {
        AudioSource[] allAudioSources = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);


        foreach (AudioSource audioSource in allAudioSources)
        {
            audioSource.volume = masterVolume;
        }
    }
    public void SetTurnSpeed(float turnSpeed)
    {
        continuousTurn.turnSpeed = turnSpeed;
    }
    public void SetMoveSpeed(float moveSpeed)
    {
        moveProvider.moveSpeed = moveSpeed;
    }
    public void ReadSettings(string settings)
    {
        string[] components = settings.Split(',');

        if (components.Length == 3)
        {
            if (float.TryParse(components[0], out volume) &&
                float.TryParse(components[1], out sensitivity) &&
                float.TryParse(components[2], out movementSpeed))
            {
                Debug.Log(PlayerPrefs.GetString("Settings"));
            }
        }
    }

    public void SetTypeFromIndex(int index)
    {
        if (snapTurn != null && continuousTurn != null)
        {
            if (index == 0)
            {
                snapTurn.enabled = true;
                continuousTurn.enabled = false;
            }
            else
            {
                snapTurn.enabled = false;
                continuousTurn.enabled = true;
            }
        }

        PlayerPrefs.SetInt("TurnType", index);
        PlayerPrefs.Save();
    }
}
