using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitGenerator : MonoBehaviour
{
    // The fruit prefab to be generated on the tree trunk
    public GameObject fruitPrefab;
    // The branch prefab that links the fruit to the tree trunk
    public GameObject branchPrefab;

    // The number of fruit prefabs to be generated on the tree trunk
    public int numberOfFruit = 100;

    // The maximum distance from the tree trunk for the fruit prefab
    public float maxOffsetDistance = 2.0f;

    public float fruitHeight = 1.0f;

    // The overall volume that the prefabs should be confined in
    public Vector3 prefabVolume = new Vector3(1.0f, 1.0f, 1.0f);

    // Returns the closest point on the tree trunk mesh to the given position
    Vector3 GetClosestPointOnMesh(Vector3 position)
    {
        // Get the mesh collider of the tree trunk
        MeshCollider meshCollider = GetComponent<MeshCollider>();

        // Get the closest point on the tree trunk mesh to the given position
        Vector3 closestPoint = meshCollider.ClosestPoint(position);

        // Return the closest point
        return closestPoint;
    }

    void Start()
    {
        // Generate the fruit prefabs on the tree trunk
        for (int i = 0; i < numberOfFruit; i++)
        {
            // Create a new fruit prefab at a random position on the tree trunk
            Vector3 offsetPosition = transform.position + Vector3.up * fruitHeight + Random.insideUnitSphere * maxOffsetDistance;
            offsetPosition.x *= prefabVolume.x;
            offsetPosition.y *= prefabVolume.y;
            offsetPosition.z *= prefabVolume.z;

            GameObject fruit = Instantiate(fruitPrefab, offsetPosition, Quaternion.identity);
            SceneManager.allFruits.Add(fruit);

            Vector3 fruitScale = new Vector3(Random.Range(0.16f, 0.24f), Random.Range(0.16f, 0.24f), Random.Range(0.16f, 0.24f));
            // Parent the fruit prefab to the tree trunk
            
            fruit.transform.localScale = fruitScale;

            // Create a branch prefab that connects the fruit to the tree trunk
            Vector3 closestPoint = GetClosestPointOnMesh(fruit.transform.position);
            Vector3 branchPosition = (fruit.transform.position + closestPoint) / 2.0f;
            GameObject branch = Instantiate(branchPrefab, branchPosition, Quaternion.identity);

            // Set the scale and rotation of the branch prefab
            Vector3 branchScale = new Vector3(0.25f, 0.25f, Vector3.Distance(fruit.transform.position, closestPoint)); 
            branch.transform.localScale = 50 * branchScale;
            branch.transform.LookAt(fruit.transform);

            SceneManager.allBranches.Add(branch);

            // Parent the fruit prefab and branch prefab to the tree trunk and add the "Fruits" tag
            fruit.transform.parent = transform;
            branch.transform.parent = transform;
            fruit.tag = "Fruits";
            fruit.layer = LayerMask.NameToLayer("Fruits");
            branch.tag = "Branches";
        }
    }
}
