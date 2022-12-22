using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrabFruitAnimation : MonoBehaviour
{
    public InputActionProperty pinchAction;
    public Animator grabAnimator;

    private float grabValue;

    // Update is called once per frame
    void Update()
    {
        grabValue = pinchAction.action.ReadValue<float>();
        if (grabValue > 0.05f)
        {
            grabAnimator.SetTrigger("grab");
        }
    }
}
