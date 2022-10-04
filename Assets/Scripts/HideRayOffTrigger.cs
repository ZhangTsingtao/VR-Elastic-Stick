using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;


public class HideRayOffTrigger : MonoBehaviour
{
    public GameObject rightTeleportationRay;
    public GameObject leftTeleportationRay;
    public InputActionProperty rightTrigger;
    public InputActionProperty leftTrigger;
    void Update()
    {
        rightTeleportationRay.SetActive(rightTrigger.action.ReadValue<float>() > 0.1f);
        leftTeleportationRay.SetActive(leftTrigger.action.ReadValue<float>() > 0.1f);

    }
}
