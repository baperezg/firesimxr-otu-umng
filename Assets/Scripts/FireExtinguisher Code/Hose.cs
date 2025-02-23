using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hose : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public GameObject[] hoseSegments; // Assign your physics segments here in the inspector

    void Update()
    {
        Vector3[] segmentPositions = new Vector3[hoseSegments.Length];
        for (int i = 0; i < hoseSegments.Length; i++)
        {
            // Collect each segment's position
            segmentPositions[i] = hoseSegments[i].transform.position;
        }

        // Update the Line Renderer to follow these positions
        lineRenderer.positionCount = segmentPositions.Length;
        lineRenderer.SetPositions(segmentPositions);
    }
}
