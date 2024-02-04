using UnityEngine;

public class AmmoManager2 : MonoBehaviour
{
    public int maxAmmo = 6; // Maximum ammunition (changed to 6)
    public int currentAmmo;   // Current ammunition
    public int fontSize = 12;    // Adjustable font size
    public Texture2D ammoIcon; // The ammunition icon texture
    public Vector2 ammoIconSize = new Vector2(20, 20); // Settable size for the ammunition icon
    public int padding = 5; // Padding between icons

    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = 6; // Set initial ammunition to 6
    }

    // Method to consume one unit of ammunition
    public void UseAmmo()
    {
        // Consume one unit of ammo per shot
        currentAmmo--;

        // Ensure ammunition doesn't go below 0
        currentAmmo = Mathf.Clamp(currentAmmo, 0, maxAmmo);

        Debug.Log("Current Ammo: " + currentAmmo);
    }

    // Method to restock ammunition
    public void RestockAmmo(int restockAmount)
    {
        currentAmmo += restockAmount;

        // Ensure ammunition doesn't exceed maxAmmo
        currentAmmo = Mathf.Clamp(currentAmmo, 0, maxAmmo);

        Debug.Log("Current Ammo: " + currentAmmo);
    }

    // Method to handle reloading logic
    public void ReloadGun()
    {
        // Set the current ammo back to the maximum ammo value (6 in this case)
        currentAmmo = 6;
        Debug.Log("Gun reloaded. Current Ammo: " + currentAmmo);
    }

    // OnGUI is called for rendering and handling GUI events.
    private void OnGUI()
    {
        GUIStyle style = new GUIStyle(GUI.skin.label);
        style.fontSize = fontSize;

        // Calculate the number of icons to display based on the current ammunition (limited to 6)
        int iconsToDisplay = Mathf.Clamp(currentAmmo, 0, 6);

        float totalWidth = iconsToDisplay * (ammoIconSize.x + padding);
        float startX = Screen.width - totalWidth;
        float startY = 0;

        // Draw the ammunition icons based on the current ammunition
        for (int i = 0; i < iconsToDisplay; i++)
        {
            float xPos = startX + i * (ammoIconSize.x + padding);
            float yPos = startY;
            GUI.DrawTexture(new Rect(xPos, yPos, ammoIconSize.x, ammoIconSize.y), ammoIcon);
        }
    }
}
