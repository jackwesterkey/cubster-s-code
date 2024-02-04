using UnityEngine;

public class Sandy3 : MonoBehaviour
{
    // Specify the target GameObject that, when collided with, will crash the game
    public GameObject targetObject;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == targetObject)
        {
            // Intentionally crash the game when colliding with the specific object
            Debug.LogError("Crashing the game!");
            // Force a null reference exception to crash the game
            object nullObject = null;
            nullObject.ToString();
        }
    }
}
