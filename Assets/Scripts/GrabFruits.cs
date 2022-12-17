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

    private GameObject theFruit;

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
                StartCoroutine(GrabAFruit());                
            }
        }
    }
    IEnumerator GrabAFruit()
    {
        yield return new WaitForSeconds(0.2f);

        int index = SceneManager.allFruits.IndexOf(theFruit);
        SceneManager.allFruits.Remove(theFruit);

        GameObject branch = SceneManager.allBranches.ElementAt(index);
        SceneManager.allBranches.Remove(branch);

        Destroy(theFruit);
        Destroy(branch);
    }
}
