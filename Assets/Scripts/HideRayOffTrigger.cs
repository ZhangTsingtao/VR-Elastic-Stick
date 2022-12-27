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

    public XRRayInteractor rightRay;
    void Update()
    {
        bool isRightRayHovering = rightRay.TryGetHitInfo(out Vector3 rightPos, out Vector3 rightNormal, out int rightNumber, out bool rightValid);

        rightTeleportationRay.SetActive(rightTrigger.action.ReadValue<float>() > 0.1f);
        leftTeleportationRay.SetActive(!isRightRayHovering && leftTrigger.action.ReadValue<float>() > 0.1f);
    }
}
