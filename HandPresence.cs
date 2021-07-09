using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
public class HandPresence : MonoBehaviour
{
    [SerializeField]
    private InputDevice targetDevice;
    // Start is called before the first frame update
    void Start()
    {
        List<InputDevice> devices = new List<InputDevice>();
        // Creates a mask filter that checks for input devices matching the characteristics right and controller, so right controller
        InputDeviceCharacteristics rightControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        // Filter through devices
        InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, devices);

        foreach (InputDevice item in devices)
        {
            Debug.Log(item.name + item.characteristics);
        }

        if (devices.Count > 0)
        {
            targetDevice = devices[0];
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool successfulRead;
        // The following function takes two parameters. The first is the type of input from the controller,
        // like a button or trigger, while the second parameter creates a NEW variable of a corresponding
        // type. Ex. bool for button and float for trigger.
        successfulRead = targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue);
        if (successfulRead && primaryButtonValue)
            Debug.Log("Pressing Primary Button");
        successfulRead = targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue);
        if (successfulRead && triggerValue > 0.1f)
            Debug.Log("Trigger Pressed " + triggerValue);
        successfulRead = targetDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 primary2DAxisValue);
        if (successfulRead && primary2DAxisValue != Vector2.zero)
            Debug.Log("Primary Joystick " + primary2DAxisValue);
    }
}
