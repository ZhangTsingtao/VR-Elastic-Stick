using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdZombie : MonoBehaviour
{
    [SerializeField] private GameObject targetBird;
    [SerializeField] private float birdSpeed = 1f;
    private float deadTimeCountDown = 15f;

    private Vector3 dirFormal;
    
    private Vector3 posFormal;

    private float rotTime = 0.0f;

    private bool hit = false;

    [SerializeField] private Animator birdAnimator;
    [SerializeField] private ParticleSystem deadParticle;
    void Start()
    {
        FindClosestBird();
        birdAnimator = GetComponentInChildren<Animator>();
        deadParticle = GetComponentInChildren<ParticleSystem>();
        StartCoroutine(DeadZombieCountDown());
    }

    void Update()
    {
        
        if (!hit)
        {
            if (targetBird != null)
            {
                if (rotTime < 1)
                {
                    transform.forward = Vector3.Lerp(dirFormal, (targetBird.transform.position - posFormal), rotTime);
                    rotTime += Time.deltaTime;
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, targetBird.transform.position, birdSpeed * Time.deltaTime);
                    transform.forward = targetBird.transform.position - transform.position;
                }



                //if (moveTimeCount < 1.0f)
                //{
                //    transform.position = Vector3.Lerp(posFormal, targetBird.transform.position, Mathf.SmoothStep(0, 1, moveTimeCount));
                //    moveTimeCount += Time.deltaTime/distanceModifier;//define the move speed
                //}
                //if (rotTimeCount < 1.0f)
                //{
                //    transform.forward = Vector3.Lerp(dirFormal, (targetBird.transform.position - transform.position), rotTimeCount);
                //    rotTimeCount += 2 * Time.deltaTime;//define the rotate speed
                //}
            }
            else
            {
                FindClosestBird();
            }
            if (BirdSpawner.allFruits.Count == 0)
            {
                Debug.Log("No Fruit!!!!");
            }
        }
    }

    public void FindClosestBird()
    {
        rotTime= 0.0f;
        dirFormal = transform.forward;
        posFormal = transform.position;

        GameObject closestBird = null;
        float distance = 1000f;
        foreach (GameObject bird in GameObject.FindGameObjectsWithTag("Birds"))   //SceneManager.allBirds
        {
            float curDistance = (bird.transform.position - transform.position).sqrMagnitude;
            if (curDistance < distance && !bird.GetComponent<Bird>().dead)
            {
                closestBird = bird;
                distance = curDistance;
            }
        }
        targetBird = closestBird;

        //if (SceneManager.allFruits.Count > 0)
        //    dirGoal = targetBird.transform.position - transform.position;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Birds"))
        {
            BirdSpawner.allBirds.Remove(other.gameObject);
            //Destroy(other.gameObject);
            other.gameObject.tag = "Untagged";
            Bird scriptB = other.gameObject.GetComponent<Bird>();
            scriptB.DeadBird();
            FindClosestBird();

            hit = true;
            StartCoroutine(HungryAgain());
        }
    }
    IEnumerator HungryAgain()
    {
        yield return new WaitForSeconds(1);
        hit = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Stick"))
        {
            DeadBirdZombie();
        }
    }
    IEnumerator DeadZombieCountDown()
    {
        yield return new WaitForSeconds(deadTimeCountDown);
        DeadBirdZombie();
    }

    private void DeadBirdZombie()
    {
        hit = true;
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        birdAnimator.SetBool("dead", true);
        deadParticle.Play();

        StartCoroutine(WaitTillIDie());
    }
    IEnumerator WaitTillIDie()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
