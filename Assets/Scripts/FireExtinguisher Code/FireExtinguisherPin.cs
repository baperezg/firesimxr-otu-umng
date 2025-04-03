 using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class FireExtinguisherPin : MonoBehaviour
{
    public Extinguisher extinguisher;         // Reference to your Extinguisher script
    public GameObject nozzleUi;               // UI that appears after pin is removed
    public GameObject pin;                    // The actual pin GameObject

    private Vector3 initialPosition;
    [SerializeField] private float pullDistance = 0.1f; // Fine-tuned pull distance
    private bool pinRemoved = false;

    void Start()
    {
        initialPosition = transform.localPosition;
        pinRemoved = false;

        if (extinguisher != null)
        {
            extinguisher.canFoam = false; // Disable foam by default
        }

        if (nozzleUi != null)
            nozzleUi.SetActive(false); // Hide nozzle UI until pin is pulled
    }

    void Update()
    {
        if (!pinRemoved)
        {
            float distance = Vector3.Distance(transform.localPosition, initialPosition);

            if (distance >= pullDistance)
            {
                RemovePin();
            }
        }
    }

    private void RemovePin()
    {
        pinRemoved = true;

        if (pin != null)
            pin.SetActive(false);

        if (nozzleUi != null)
            nozzleUi.SetActive(true);

        if (extinguisher != null)
            extinguisher.canFoam = true;

        Debug.Log("Pin removed, extinguisher activated.");
    }
}
