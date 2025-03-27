

using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelName : MonoBehaviour
{
    // Reference to the TextMeshPro component
    public TextMeshProUGUI textMeshPro;  // For UI TextMeshPro
    // For world-space TextMeshPro, use TextMeshPro instead

    void Start()
    {
        if (textMeshPro != null)
        {
            // Get the current scene name
            string sceneName = SceneManager.GetActiveScene().name;
            // Set the scene name to the text component
            textMeshPro.text = "" + sceneName;
        }
        else
        {
            Debug.LogError("TextMeshPro component is not assigned.");
        }
    }
}