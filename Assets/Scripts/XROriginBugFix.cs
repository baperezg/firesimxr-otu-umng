using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class XROriginBugFix : MonoBehaviour
{
    XROrigin playerOrigin;
    private void Start()
    {
        StartCoroutine(delay());
    }
    IEnumerator delay()
    {
        yield return new WaitForSeconds(1f);
        playerOrigin = GetComponent<XROrigin>();
        playerOrigin.RequestedTrackingOriginMode = XROrigin.TrackingOriginMode.Floor;
    }


}
