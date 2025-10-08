using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private float sceneFadeDuration = 1f;
    [SerializeField] private Screenfader screenfader;

    void Awake()
    {
        if (screenfader == null) screenfader = GetComponentInChildren<Screenfader>(true);
        DontDestroyOnLoad(gameObject);

        // Fade in every time a new scene finishes loading
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (screenfader != null)
            StartCoroutine(screenfader.FadeInCoroutine(sceneFadeDuration));
    }

    private IEnumerator Start()
    {
        if (screenfader != null)
            yield return screenfader.FadeInCoroutine(sceneFadeDuration);
    }

    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(LoadSceneCoroutine(sceneIndex));
    }

    private IEnumerator LoadSceneCoroutine(int sceneIndex)
    {
        // 1) Fade OUT
        if (screenfader != null)
            yield return screenfader.FadeOutCoroutine(sceneFadeDuration);

        // 2) NOW actually load the scene
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Single);
        op.allowSceneActivation = true;
        yield return op; // OnSceneLoaded will trigger FadeIn
    }
}
