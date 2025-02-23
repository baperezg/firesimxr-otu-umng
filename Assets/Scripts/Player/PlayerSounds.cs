using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{

    public AudioSource footSteps;
    private Vector3 lastPosition;

    private void Start()
    {
        lastPosition = transform.position;
    }
    private void Update()
    {
        if (IsMoving())
        {
            footSteps.enabled = true;
        }
        else
        {
            footSteps.enabled = false;
        }
    }
    bool IsMoving()
    {

        // Check if the current position is different from the last position
        if (transform.position != lastPosition)
        {
            // Update lastPosition to the current position
            lastPosition = transform.position;
            return true;
        }
        return false;
    }
}
