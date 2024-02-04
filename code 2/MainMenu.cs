using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Add this line to use the UI components
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // ... (your existing code)

    public void LoadGame()
    {
        SceneManager.LoadScene("cubester 1");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
