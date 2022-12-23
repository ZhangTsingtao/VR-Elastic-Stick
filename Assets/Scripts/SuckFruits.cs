using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;

public class SuckFruits : MonoBehaviour
{
    public InputActionProperty pinchAction;
    private float suckValue;

    public float speed = 5f;
    public float destroyDistance = 0.1f;
    
    public GameObject vacuumeCenter;
    
    public ParticleSystem vacuumDust;
    public ParticleSystem gotFruitPar;
    [SerializeField] private GameObject theFruit;
    [SerializeField] private GameObject theBranch;
    void Update()
    {
        suckValue = pinchAction.action.ReadValue<float>();

        vacuumDust.emissionRate = suckValue * 15f;
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Fruits"))
        {
            if(suckValue > 0.2f)
            {
                theFruit = other.gameObject;
                theBranch = theFruit.GetComponent<ActiveRigOnHit>().relatedBranch;


                Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
                if(other.GetComponent<ActiveRigOnHit>().oncePlayed)
                {
                    rb.isKinematic = true;
                    other.gameObject.transform.position = Vector3.MoveTowards(other.gameObject.transform.position, vacuumeCenter.transform.position, suckValue * speed * Time.deltaTime);
                    gotFruitPar.Play();

                    float distance = Vector3.Distance(vacuumeCenter.transform.position, other.transform.position);
                    Debug.Log(distance);
                    if(distance < destroyDistance)
                    {
                        GrabAFruit();
                    }
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Fruits"))
        {
            if (other.GetComponent<ActiveRigOnHit>().oncePlayed)
            {
                Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
                rb.isKinematic = false;
                //Debug.Log("Now it's set to none Kinematic");
            }
        }
    }
    public void GrabAFruit()
    {
        SceneManager.allFruits.Remove(theFruit);
        Destroy(theFruit);
        Destroy(theBranch);

    }
}
