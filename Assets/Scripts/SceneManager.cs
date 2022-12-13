using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public static List<GameObject> allFruits = new List<GameObject>();
    public static List<GameObject> allBirds = new List<GameObject>();
    public static List<GameObject> allBranches = new List<GameObject>();


    [SerializeField] private int fruitCount = 100;
    public GameObject birdPref;
    public GameObject fruitPref;

    private float timer = 0.0f;
    [SerializeField] private float spawnBirdPeriod = 1.0f;

    private void Start()
    {
        for (int i = 0; i < fruitCount; i++)
        {
            Vector3 randomSpawnPos = new Vector3(Random.Range(-2.0f, 3.0f), Random.Range(2.0f, 5.0f), Random.Range(-1.0f, 3.0f));
            Quaternion randomSpawnRot = Quaternion.Euler(new Vector3(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f)));
            var fruitInstance = Instantiate(fruitPref, randomSpawnPos, randomSpawnRot);
            allFruits.Add(fruitInstance);
        }

        //foreach (GameObject go in GameObject.FindGameObjectsWithTag("Fruits"))
        //{
        //    allFruits.Add(go);
        //}
        Debug.Log(allFruits.Count);

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

        Vector3 randomSpawnPos = new Vector3(Random.Range(-15, 16), Random.Range(2, 6), Random.Range(3, 16));
        
        var birdInstance = Instantiate(birdPref, randomSpawnPos, Quaternion.identity);
        birdInstance.AddComponent<Bird>();
        allBirds.Add(birdInstance);

    }
}
