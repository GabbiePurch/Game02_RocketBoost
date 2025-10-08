using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI gameText;
    int fuel = 0;
    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Everything Looks Good");
                break;
            case "Finish":
                LoadNextLevel();
                break;
            case "Fuel":
                fuel = fuel + 5;
                gameText.text = "Fuel: " + fuel;
                Destroy(other.gameObject);
                break;
            default:
                ReloadLevel();
                break;
        }
    }

    void LoadNextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;

        if (nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }

        SceneManager.LoadScene(nextScene);
    }

    
    void ReloadLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

}
