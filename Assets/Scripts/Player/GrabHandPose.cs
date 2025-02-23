using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

#if UNITY_EDITOR
using UnityEditor;
#endif
public class GrabHandPose : MonoBehaviour
{
    public HandData leftHandPose;
    public HandData rightHandPose;

    private Vector3 startHandPosition;
    private Vector3 endHandPosition;
    private Quaternion startHandRotation;
    private Quaternion endHandRotation;

    private Quaternion[] startingFingerRotations;
    private Quaternion[] endFingerRotations;
    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        grabInteractable.selectEntered.AddListener(SetupPose);
        grabInteractable.selectExited.AddListener(UnSetPose);

        leftHandPose.gameObject.SetActive(false);
        rightHandPose.gameObject.SetActive(false);
    }

    public void SetupPose(BaseInteractionEventArgs arg)
    {
        if(arg.interactorObject is UnityEngine.XR.Interaction.Toolkit.Interactors.XRDirectInteractor)
        {
            HandData handData = arg.interactorObject.transform.GetComponentInChildren<HandData>();
            handData.animator.enabled = false;

            if(handData.handType == HandData.HandModelType.Left)
            {
                SetHandDataValues(handData, leftHandPose);
            }
            else
            {
                SetHandDataValues(handData, rightHandPose);
            }

            SetHandData(handData, endHandPosition, endHandRotation, endFingerRotations);
        }
    }

    public void UnSetPose(BaseInteractionEventArgs arg)
    {
        if (arg.interactorObject is UnityEngine.XR.Interaction.Toolkit.Interactors.XRDirectInteractor)
        {
            HandData handData = arg.interactorObject.transform.GetComponentInChildren<HandData>();
            handData.animator.enabled = true;

            SetHandData(handData, startHandPosition, startHandRotation, startingFingerRotations);
        }
    }
    public void SetHandDataValues(HandData h1, HandData h2)
    {
        startHandPosition = new Vector3(h1.root.localPosition.x / h1.root.localScale.x, h1.root.localPosition.y / h1.root.localScale.y, h1.root.localPosition.z / h1.root.localScale.z);
        endHandPosition =   new Vector3(h2.root.localPosition.x / h2.root.localScale.x, h2.root.localPosition.y / h2.root.localScale.y, h2.root.localPosition.z / h2.root.localScale.z);

        startHandRotation = h1.root.localRotation;
        endHandRotation = h2.root.localRotation;

        startingFingerRotations = new Quaternion[h1.fingerBones.Length];
        endFingerRotations = new Quaternion[h1.fingerBones.Length];

        for(int i = 0; i < h1.fingerBones.Length; i++)
        {
            startingFingerRotations[i] = h1.fingerBones[i].localRotation;
            endFingerRotations[i] = h2.fingerBones[i].localRotation;
        }
    }

    public void SetHandData(HandData h, Vector3 newPos, Quaternion newRot, Quaternion[] newBonesRotation)
    {
        h.root.localPosition = newPos;
        h.root.localRotation = newRot;

        for (int i = 0; i < newBonesRotation.Length; i++)
        {
            h.fingerBones[i].localRotation = newBonesRotation[i];
        }
    }

#if UNITY_EDITOR

    [MenuItem("Tools/Mirror Selected Right Grab Pose")]
    public static void MirrorRightPose()
    {
        Debug.Log("Pose has been mirrored");
        GrabHandPose handPose = Selection.activeGameObject.GetComponent<GrabHandPose>();

        handPose.MirrorPose(handPose.leftHandPose, handPose.rightHandPose);
    }
    
    [MenuItem("Tools/Mirror Selected Left Grab Pose")]
    public static void MirrorLeftPose()
    {
        Debug.Log("Pose has been mirrored");
        GrabHandPose handPose = Selection.activeGameObject.GetComponent<GrabHandPose>();

        handPose.MirrorPose(handPose.rightHandPose, handPose.leftHandPose);
    }
#endif
    public void MirrorPose(HandData poseToMirror, HandData poseUsedToMirror)
    {
        Vector3 mirroredPosition = poseUsedToMirror.root.localPosition;
        mirroredPosition.x *= -1;

        Quaternion mirroredQuaternion = poseUsedToMirror.root.localRotation;
        mirroredQuaternion.y *= -1;
        mirroredQuaternion.z *= -1;

        poseToMirror.root.localPosition = mirroredPosition;
        poseToMirror.root.localRotation = mirroredQuaternion;

        for (int i = 0; i < poseToMirror.fingerBones.Length; i++)
        {
            poseToMirror.fingerBones[i].localRotation = poseUsedToMirror.fingerBones[i].localRotation;
        }
    }
}
