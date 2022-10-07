using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutIntoBasket : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fruits"))
        {
            //Debug.Log("A fruit!");
            other.gameObject.GetComponent<ActiveRigOnHit>().PutMeIn();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Fruits"))
        {
            other.gameObject.GetComponent<ActiveRigOnHit>().GetMeOut();
        }
    }
}
