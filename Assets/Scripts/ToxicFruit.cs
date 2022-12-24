using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicFruit : MonoBehaviour
{
    public GameObject birdZombie;
    [SerializeField] private ParticleSystem toxicParticle;
    [SerializeField] private GameObject emptyParticleObject;
    private void Start()
    {
        toxicParticle = GetComponentInChildren<ParticleSystem>();
        emptyParticleObject = toxicParticle.gameObject;

    }
    public void BirdToZombie(GameObject bird)
    {
        if (bird.CompareTag("Birds"))
        {
            //Debug.Log("I'm Toxic!!!!");

            emptyParticleObject.transform.SetParent(null);
            emptyParticleObject.GetComponent<ParticleSystem>().Play();
            StartCoroutine(DestroyParticle());

            Destroy(bird);
            GameObject zombie = Instantiate(birdZombie, transform.position, transform.rotation);
            zombie.tag = "BirdZombies";
            zombie.layer = LayerMask.NameToLayer("Birds");

        }
    }
    IEnumerator DestroyParticle()
    {
        yield return new WaitForSeconds(1);
        Destroy(emptyParticleObject);
    }
}
