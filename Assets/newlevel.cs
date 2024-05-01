using UnityEngine;
using UnityEngine.SceneManagement;

public class newlevel : MonoBehaviour
{
    // Array to hold the names of the scenes to load
    public string[] sceneNames;

    // Index to keep track of the current scene
    private int currentSceneIndex = 0;

    // Function to load the next scene
    public void LoadNextScene()
    {
        // Increment the current scene index
        //currentSceneIndex++;
        SceneManager.LoadScene(1);
        return;

        // Check if the current scene index is within the bounds of the sceneNames array
        if (currentSceneIndex < sceneNames.Length)
        {
            // Load the next scene
            SceneManager.LoadScene(sceneNames[currentSceneIndex]);
        }
        else
        {
            // Log a message if there are no more scenes to load
            Debug.Log("No more scenes to load.");
        }
    }

    // Function to quit the game
    public void QuitGame()
    {
        // Quit the application
        Application.Quit();
    }
}
