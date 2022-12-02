using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class Magnet : MonoBehaviour
{
    public static List<GameObject> activeFruits= new List<GameObject>();
    [SerializeField]
    private float magForce;
    [SerializeField]
    private float forceMultiplier = 5;

    private float magForceFinal;

    
    public InputActionProperty pinchAction;

    private void Update()
    {
        magForce = pinchAction.action.ReadValue<float>();
    }
    void FixedUpdate()
    {
        if(magForce > 0.1)
        {            
            foreach (GameObject fruit in activeFruits)
            {
                Vector3 direction = (gameObject.transform.position - fruit.transform.position);
                magForceFinal = magForce * forceMultiplier * 1 / direction.magnitude;
                Debug.Log(magForceFinal);

                fruit.GetComponent<Rigidbody>().AddForce(magForceFinal * direction.normalized, ForceMode.Force);
                Debug.Log(fruit.name);
            }
        }
    }
}
