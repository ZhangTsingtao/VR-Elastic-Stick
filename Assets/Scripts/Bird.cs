using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Bird : MonoBehaviour
{
    [SerializeField] private GameObject targetFruit;
    [SerializeField] private float birdSpeed = 1f;

    private float moveTimeCount = 0.0f;
    private float distanceModifier;
    private float rotTimeCount = 0.0f;
    private Vector3 posFormal;
    private Vector3 posGoal;
    private Vector3 dirFormal;
    private Vector3 dirGoal;

    private bool hit = false;
    public bool dead = false;

    [SerializeField] private Animator birdAnimator;
    [SerializeField] private ParticleSystem deadParticle;


    void Start()
    {
        FindClosestFruit();
        birdAnimator = GetComponentInChildren<Animator>();
        deadParticle = GetComponentInChildren<ParticleSystem>();

    }

    void Update()
    {
        
        if (!hit && !dead)
        {
            if (targetFruit != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetFruit.transform.position, birdSpeed * Time.deltaTime);
                //transform.forward = targetFruit.transform.position - transform.position;
                //if (moveTimeCount < 1.0f)
                //{
                //    transform.position = Vector3.Lerp(posFormal, posGoal, Mathf.SmoothStep(0, 1, moveTimeCount));
                //    moveTimeCount += Time.deltaTime/distanceModifier;//define the move speed
                //}
                
                if (rotTimeCount < 1.0f)
                {
                    transform.forward = Vector3.Lerp(dirFormal, dirGoal, rotTimeCount);
                    rotTimeCount += 2 * Time.deltaTime;//define the rotate speed
                }
                else
                {
                    transform.forward = targetFruit.transform.position - transform.position;
                }
            }
            else
            {
                FindClosestFruit();
            }
            if (BirdSpawner.allFruits.Count == 0)
            {
                Debug.Log("No Fruit!!!!");
            }
        }
    }

    public void FindClosestFruit()
    {
        moveTimeCount = 0.0f;
        rotTimeCount = 0.0f;
        posFormal = transform.position;
        dirFormal = transform.forward;

        GameObject closestFruit = null;
        float distance = 1000f;
        foreach (GameObject fruit in BirdSpawner.allFruits)
        {
            float curDistance = (fruit.transform.position - transform.position).sqrMagnitude;
            if (curDistance < distance)
            {
                closestFruit = fruit;
                distance = curDistance;
            }
        }
        targetFruit = closestFruit;

        if (targetFruit != null)
        {
            posGoal = targetFruit.transform.position;
            distanceModifier = (posGoal - posFormal).magnitude/birdSpeed;
            dirGoal = targetFruit.transform.position - transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fruits"))
        {
            BirdSpawner.allFruits.Remove(other.gameObject);

            GameObject branch = other.gameObject.GetComponent<ActiveRigOnHit>().relatedBranch;

            Destroy(other.gameObject);
            Destroy(branch);

            if(other.gameObject.GetComponent<ToxicFruit>() != null)
            {
                other.gameObject.GetComponent<ToxicFruit>().BirdToZombie(gameObject);
            }
            else
            {
                GameObject gameObject = GameObject.Find("HandleVacuum");
                gameObject.GetComponent<SuckFruits>().MinusScore();
            }

            FindClosestFruit();

            hit = true;
            if(!dead)
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
            DeadBird();
        }
    }


    public void DeadBird()
    {
        hit = true;
        dead = true;
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        birdAnimator.SetBool("dead", true);
        deadParticle.Play();

        StartCoroutine(WaitTillIDie());
    }
    IEnumerator WaitTillIDie()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }
}
