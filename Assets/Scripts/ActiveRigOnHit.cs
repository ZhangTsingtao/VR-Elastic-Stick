using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class ActiveRigOnHit : MonoBehaviour
{
    private Rigidbody rb;
    private XRGrabInteractable grabable;

    public GameObject basket;
    public GameObject relatedBranch;

    public bool oncePlayed = false;

    private void Start()
    {
        grabable = GetComponent<XRGrabInteractable>();
        grabable.enabled = false;

        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Stick"))
        {
            rb.isKinematic = false;
            grabable.enabled = true;

            Magnet.activeFruits.Add(gameObject);
           
            if (!oncePlayed)
            {
                PlayEffects();
                oncePlayed = true;
            }
            
        }
    }
    public void PlayEffects()
    {
        transform.gameObject.GetComponentInChildren<ParticleSystem>().Play();
    }

    public void PutMeIn()
    {
        gameObject.transform.parent = basket.transform;
        //Debug.Log("in the basket");
    }

    public void GetMeOut()
    {
        gameObject.transform.parent = GameObject.Find("Fruits").transform;
        //Debug.Log("out the basket");
    }
}
