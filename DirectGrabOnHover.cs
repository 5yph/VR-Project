using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DirectGrabOnHover : MonoBehaviour
{
    private XRRayInteractor Ray;
    private float originalMax;
    private int grabLayer;
    private XRInteractorLineVisual line;
    private float originalLength;
    // Start is called before the first frame update
    void Start()
    {
        Ray = GetComponent<XRRayInteractor>();
        originalMax = Ray.maxRaycastDistance;
        grabLayer = LayerMask.NameToLayer("Grab");
        line = GetComponent<XRInteractorLineVisual>();
        originalLength = line.lineLength;
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out var hit, Mathf.Infinity, 1 << grabLayer))
        {
            Debug.Log("Changed to grab distance");
            Ray.maxRaycastDistance = 0.3f;
            line.lineLength = 0;
        }
        else
        {
            Debug.Log("Changed to original distance");
            Ray.maxRaycastDistance = originalMax;
            line.lineLength = originalLength;
        }
    }
}
