using UnityEngine;
using UnityEngine.SceneManagement;

public class HeartVisibilityController : MonoBehaviour
{
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;

    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        DontDestroyOnLoad(gameObject);
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string currentScene = scene.name;

        bool showHearts = currentScene == "Room0" ||
                          currentScene == "Room1" ||
                          currentScene == "Room2" ||
                          currentScene == "Room3" ||
                          currentScene == "Room4" ||
                          currentScene == "Room5";

        heart1.SetActive(showHearts);
        heart2.SetActive(showHearts);
        heart3.SetActive(showHearts);
    }
}