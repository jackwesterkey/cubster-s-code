using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sandy : MonoBehaviour
{
    // OnDestroy is called when the GameObject is destroyed
    void OnDestroy()
    {
        // Load the MainMenu scene
        SceneManager.LoadScene("doom like 1");
    }
}
