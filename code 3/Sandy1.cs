using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sandy1 : MonoBehaviour
{
    // OnDestroy is called when the GameObject is destroyed
    void OnDestroy()
    {
        // Load the MainMenu scene
        SceneManager.LoadScene("last game");
    }
}
