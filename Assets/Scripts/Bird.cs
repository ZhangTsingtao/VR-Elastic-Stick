using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] private GameObject targetFruit;
    [SerializeField] private float birdSpeed = 1.5f;

    private bool hit = false;
    void Start()
    {
        FindClosestFruit();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hit && targetFruit != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetFruit.transform.position, birdSpeed * Time.deltaTime);
            transform.forward = targetFruit.transform.position - transform.position;
        }
        else if (targetFruit = null)
        { 
            FindClosestFruit();
            Debug.Log(gameObject.name + "says: I can't find my fruit!");
        }
    }

    public void FindClosestFruit()
    {
        GameObject closestFruit = null;
        float distance = 1000f;
        foreach (GameObject fruit in SceneManager.allFruits)
        {
            float curDistance = (fruit.transform.position - transform.position).sqrMagnitude;
            if (curDistance < distance)
            {
                closestFruit = fruit;
                distance = curDistance;
            }
        }
        targetFruit = closestFruit;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fruits"))
        {
            SceneManager.allFruits.Remove(other.gameObject);
            Destroy(other.gameObject);
            FindClosestFruit();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Stick"))
        {
            hit = true;
            gameObject.GetComponent<Rigidbody>().isKinematic = false;

            StartCoroutine(WaitTillIDie());
        }
    }
    IEnumerator WaitTillIDie()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
