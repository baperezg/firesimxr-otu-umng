using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    public Transform target;

    void Update()
    {
        transform.LookAt(target, Vector3.up);
        transform.Rotate(0, 180, 0);
    }
}
