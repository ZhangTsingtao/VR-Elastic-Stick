using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicFruit : MonoBehaviour
{
    public GameObject birdZombie;
    public void BirdToZombie(GameObject bird)
    {
        if (bird.CompareTag("Birds"))
        {
            Debug.Log("I'm Toxic!!!!");
            Destroy(bird);
            GameObject zombie = Instantiate(birdZombie, transform.position, transform.rotation);
            zombie.tag = "BirdZombies";
        }
    }
}
