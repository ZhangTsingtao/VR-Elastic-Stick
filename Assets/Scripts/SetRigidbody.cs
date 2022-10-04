using UnityEngine;
using System.Collections;

public class SetRigidbody : MonoBehaviour
{
    public Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, 0, 0);
        rb.inertiaTensor = new Vector3(1, 1, 1);
    }
}