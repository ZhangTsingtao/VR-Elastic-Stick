using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public void RestartGame()
    {
        // Get the current scene by its build index
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Load the scene by its build index
        SceneManager.LoadScene(sceneIndex);
    }
}