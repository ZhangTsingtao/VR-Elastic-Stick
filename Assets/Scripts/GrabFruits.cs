using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrabFruits : MonoBehaviour
{
    public InputActionProperty pinchAction;

    private float grabValue;

    [SerializeField] private GameObject theFruit;
    [SerializeField] private GameObject theBranch;

    void Update()
    {
        grabValue = pinchAction.action.ReadValue<float>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Fruits"))
        {
            if(grabValue > 0.2f)
            {
                theFruit = other.gameObject;
                theBranch = theFruit.GetComponent<ActiveRigOnHit>().relatedBranch;
                StartCoroutine(GrabAFruit());

            }
        }
    }
    IEnumerator GrabAFruit()
    {
        yield return new WaitForSeconds(0.2f);

        SceneManager.allFruits.Remove(theFruit);
        Destroy(theFruit);
        Destroy(theBranch);

    }
}
