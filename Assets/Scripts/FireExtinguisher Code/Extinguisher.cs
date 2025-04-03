using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;
public class Extinguisher : MonoBehaviour
{
    public ParticleSystem foamParticle;
    public bool canFoam = true;
    [SerializeField] private float amountExtinguishedPerSecond = 1.0f;
    [SerializeField] private bool isSpraying = false;

    [SerializeField] private AudioSource spraySound;
    [SerializeField] private LayerMask fireLayer;

    // Foam capacity logic
    [Header("Foam Capacity Settings")]
    [SerializeField] private float maxFoamDuration = 5.0f; // seconds of spray time
    private float currentFoamAmount;

    public ExtinguisherType extinguisherType;
    public enum ExtinguisherType
    {
        Wood,
        Electrical
    }

    void Start()
    {
        currentFoamAmount = maxFoamDuration;

        var grabbable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        grabbable.activated.AddListener(Foam);
        grabbable.deactivated.AddListener(StopFoam);

        spraySound = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // Extinguish logic
        if (isSpraying)
        {
            if (currentFoamAmount > 0f)
            {
                currentFoamAmount -= Time.deltaTime;

                if (Physics.Raycast(this.transform.position, this.transform.forward, out RaycastHit hit, 100f)
                    && hit.collider.TryGetComponent(out Fire fire))
                {
                    Debug.Log(hit.collider.name);
                    fire.TryExtinguish(amountExtinguishedPerSecond * Time.deltaTime, extinguisherType);
                }
            }
            else
            {
                // Out of foam
                canFoam = false;
                isSpraying = false;
                foamParticle.Stop();
                spraySound.Stop();
                Debug.Log("Out of foam!");
            }
        }
    }

    public void Foam(ActivateEventArgs arg)
    {
        if (canFoam && currentFoamAmount > 0f)
        {
            isSpraying = true;
            foamParticle.Play();
            spraySound.Play();
        }
    }

    public void StopFoam(DeactivateEventArgs arg)
    {
        if (isSpraying)
        {
            isSpraying = false;
            foamParticle.Stop();
            spraySound.Stop();
        }
    }
}
