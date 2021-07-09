using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class TeleportGrabSwitch : MonoBehaviour
{
    public enum LeftOrRight
    {
        None,
        Left,
        Right
    };

    public LeftOrRight Controller;

    public bool Teleport = true;
    
    private XRIDefaultInputActions controls;
    private XRRayInteractor Ray;
    private LayerMask originalLayers;
    private LayerMask grabLayer;
    //private InputActionProperty switchButton;

    private void Awake()
    {
        if (Controller == LeftOrRight.None)
            throw new System.ArgumentException("Specify Left or Right Controller");

        controls = new XRIDefaultInputActions();
        // Default button to switch between teleport and grab is Trigger
        //ActionBasedController controller = GetComponent<ActionBasedController>();
        //switchButton = controller.activateAction;
        //switchButton.action.performed += SwitchAction;
        if (Controller == LeftOrRight.Left)
            controls.XRILeftHand.TeleportModeActivate.performed += ctx => SwitchAction();
        else
            controls.XRIRightHand.TeleportModeActivate.performed += ctx => SwitchAction();

        
    }
    // Start is called before the first frame update
    void Start()
    {
        
        Ray = GetComponent<XRRayInteractor>();
        originalLayers = Ray.raycastMask.value;
        grabLayer = LayerMask.NameToLayer("Grab");
        Debug.Log("Grab layer: " + grabLayer.value);

        if (Teleport)
            Ray.lineType = XRRayInteractor.LineType.ProjectileCurve;
    }

    public void SwitchAction()
    {
        Debug.Log("Switched action");
        Teleport = !Teleport;
        if (Teleport)
        {
            Ray.raycastMask = originalLayers;
            Debug.Log("Layer: " + Ray.raycastMask.value);
            Ray.lineType = XRRayInteractor.LineType.ProjectileCurve;
        }
        else
        {
            Ray.raycastMask = 1 << grabLayer;
            Debug.Log("Layer: " + Ray.raycastMask.value);
            Ray.lineType = XRRayInteractor.LineType.StraightLine;
        }
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

}
