using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGroundOutBasket : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Fruits"))
        {
            collision.transform.parent = gameObject.transform;
            Debug.Log("!");
        }
    }
}
