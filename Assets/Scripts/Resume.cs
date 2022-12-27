using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resume : MonoBehaviour
{
    public GameObject menu;
    public void ResumeGame()
    {
        menu.SetActive(false);
    }
}
