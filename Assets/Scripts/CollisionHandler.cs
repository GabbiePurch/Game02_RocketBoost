using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using NUnit.Framework;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.RenderGraphModule;
using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI gameText;
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip sucessSFX;
    [SerializeField] AudioClip crashSFX;
    [SerializeField] ParticleSystem sucessParticals;
    [SerializeField] ParticleSystem crashParticals;
    [SerializeField] float sceneFadeDuration = 1f;
    [SerializeField] SceneController sceneController;
    

    AudioSource audioSource;


    bool isControlable = true;
    bool isCollidable = true;
    int fuel = 0;
    int currentScene;


    void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        audioSource = GetComponent<AudioSource>();

    }

    void Update()
    {
        RespondtoDebugKeys();
    }

    void RespondtoDebugKeys()
    {
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            LoadNextLevel();
        }

        else if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            isCollidable = !isCollidable;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!isControlable || !isCollidable) { return; }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Everything Looks Good");
                break;
            case "Finish":
                StartSucessSequence();
                break;
            case "Fuel":
                fuel = fuel + 5;
                gameText.text = "Fuel: " + fuel;
                Destroy(other.gameObject);
                break;
            default:
                StartCrashSequence();
                break;
        }
    }
    
    void StartCrashSequence()
    {
        isControlable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(crashSFX);
        crashParticals.Play();
        GetComponent<Movement>().enabled = false;
        sceneController.LoadScene(currentScene);
    }

    void StartSucessSequence()
    {

        isControlable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(sucessSFX);
        sucessParticals.Play();
        GetComponent<Movement>().enabled = false;
        sceneController.LoadScene(currentScene + 1);
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
