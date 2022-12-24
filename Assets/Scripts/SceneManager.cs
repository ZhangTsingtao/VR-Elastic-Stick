using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public static List<GameObject> allFruits = new List<GameObject>();
    public static List<GameObject> allBirds = new List<GameObject>();
    public GameObject birdPref;
    
    private float timer = 0.0f;
    public float spawnBirdPeriod = 1.0f;

    public Vector3 birdSpawnPos = new Vector3(0f, 4f, 8f);
    public Vector3 birdSpawnDensity = new Vector3(3f, 1f, 3f);

    private float randomXmin;
    private float randomYmin;
    private float randomZmin;
    private float randomXmax;
    private float randomYmax;
    private float randomZmax;

    private void Start()
    {
        randomXmin = birdSpawnPos.x - birdSpawnDensity.x;
        randomXmax = birdSpawnPos.x + birdSpawnDensity.x;
        randomYmin = birdSpawnPos.y - birdSpawnDensity.y;
        randomYmax = birdSpawnPos.y + birdSpawnDensity.y;
        randomZmin = birdSpawnPos.z - birdSpawnDensity.z;
        randomZmax = birdSpawnPos.z + birdSpawnDensity.z;
    }

    private void Update()
    {
        if (timer > spawnBirdPeriod && allFruits.Count > 0)
        {
            RandomSpawn();
            timer = 0;
        }
        timer += Time.deltaTime;


    }

    private void RandomSpawn()
    {
        

        Vector3 randomSpawnPos = new Vector3(Random.Range(randomXmin, randomXmax), Random.Range(randomYmin, randomYmax), Random.Range(randomZmin, randomZmax));
        
        var birdInstance = Instantiate(birdPref, randomSpawnPos, Quaternion.identity);
        birdInstance.AddComponent<Bird>();
        birdInstance.layer = LayerMask.NameToLayer("Birds");

        allBirds.Add(birdInstance);

    }
}
