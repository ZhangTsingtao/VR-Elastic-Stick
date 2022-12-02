using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public static List<GameObject> allFruits = new List<GameObject>();

    public GameObject birdPref;
    private float timer = 0.0f;
    [SerializeField] private float period = 1.0f;

    void Awake()
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Fruits"))
        {
            allFruits.Add(go);
        }
    }

    private void Update()
    {
        if (timer > period)
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

    }
}
