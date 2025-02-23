using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
public class Extinguisher : MonoBehaviour
{
    public ParticleSystem foamParticle;
    public bool canFoam = false;
    [SerializeField] private float amountExtinguishedPerSecond = 1.0f;
    [SerializeField] private bool isSpraying = false;

    [SerializeField] private AudioSource spraySound;
    [SerializeField] private LayerMask fireLayer;

    public XRBaseController leftHand;
    public XRBaseController rightHand;

    public ExtinguisherType extinguisherType;
    public enum ExtinguisherType
    {
        Wood,
        Electrical
    }

    void Start()
    {
        UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabbable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        grabbable.activated.AddListener(Foam);

        grabbable.deactivated.AddListener(StopFoam);

        spraySound = GetComponent<AudioSource>();
    }
    
    private void Update()
    {
        if (Physics.Raycast(this.transform.position, this.transform.forward, out RaycastHit hit, 100f)
            && hit.collider.TryGetComponent(out Fire fire) && isSpraying)
        {
            Debug.Log(hit.collider.name);
            fire.TryExtinguish(amountExtinguishedPerSecond * Time.deltaTime, extinguisherType);
        }

        if (isSpraying)
        {
            rightHand.SendHapticImpulse(0.5f, 0.1f);
            leftHand.SendHapticImpulse(0.5f, 0.1f);

        }
    }
    public void Foam(ActivateEventArgs arg)
    {
        if (canFoam)
        { 
            isSpraying = true;
            foamParticle.Play();
            spraySound.Play();
        }
    }

    public void StopFoam(DeactivateEventArgs arg)
    {
        if (canFoam)
        {
            isSpraying = false;
            foamParticle.Stop();
            spraySound.Stop();
        }
    }
}
