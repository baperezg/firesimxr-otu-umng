using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FireExtinguisherPin : MonoBehaviour
{
    public Extinguisher extinguisher;
    private Vector3 initialPosition;
    private float pullDistance = 1f; 
    private bool pinRemoved = false;
    public GameObject nozzleUi, pin;

    void Start()
    {
        pinRemoved = false;
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        // Check if the pin has been pulled out enough
        if (!pinRemoved && Mathf.Abs(transform.localPosition.z - initialPosition.z) > pullDistance)
        {
            pin.SetActive(false);
            nozzleUi.SetActive(true);
            pinRemoved = true;
            extinguisher.canFoam = true; // Enable foaming
            Debug.Log("Pin removed, extinguisher activated.");

        }
    }
}
