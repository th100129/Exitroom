using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class HeartManager : MonoBehaviour
{
    public Image Heart3;
    public Image Heart2;
    public Image Heart1;
    public Image whiteFadeImage;
    public float fadeDuration = 2f;

    private int heartCount = 3;
    private float lastClickTime = 0f;
    private float doubleClickDelay = 0.3f;

    private static HeartManager instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject); 
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Time.time - lastClickTime < doubleClickDelay)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.collider.tag != "SceneTrigger")
                    {
                        DecreaseHeart();
                    }
                }
            }
            lastClickTime = Time.time;
        }
    }

    void DecreaseHeart()
    {
        if (heartCount == 3 && Heart3 != null) Heart3.gameObject.SetActive(false);
        else if (heartCount == 2 && Heart2 != null) Heart2.gameObject.SetActive(false);
        else if (heartCount == 1 && Heart1 != null)
        {
            Heart1.gameObject.SetActive(false);
            StartCoroutine(FadeAndLoadRoom0());
        }

        heartCount--;
    }

    IEnumerator FadeAndLoadRoom0()
    {
        float elapsed = 0f;
        Color c = whiteFadeImage.color;

        while (elapsed < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsed / fadeDuration);
            whiteFadeImage.color = new Color(c.r, c.g, c.b, alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }

        whiteFadeImage.color = new Color(c.r, c.g, c.b, 1f);
        SceneManager.LoadScene("Room0");
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string sceneName = scene.name;

        bool isRoomScene = sceneName == "Room0" || sceneName == "Room1" ||
                           sceneName == "Room2" || sceneName == "Room3" ||
                           sceneName == "Room4" || sceneName == "Room5";

        Heart3 = GameObject.Find("Heart3")?.GetComponent<Image>();
        Heart2 = GameObject.Find("Heart2")?.GetComponent<Image>();
        Heart1 = GameObject.Find("Heart1")?.GetComponent<Image>();
        whiteFadeImage = GameObject.Find("WhiteFade")?.GetComponent<Image>();

        if (Heart3 != null) Heart3.gameObject.SetActive(isRoomScene && heartCount >= 3);
        if (Heart2 != null) Heart2.gameObject.SetActive(isRoomScene && heartCount >= 2);
        if (Heart1 != null) Heart1.gameObject.SetActive(isRoomScene && heartCount >= 1);

        if (sceneName == "Room0")
        {
            heartCount = 3;
            if (Heart3 != null) Heart3.gameObject.SetActive(true);
            if (Heart2 != null) Heart2.gameObject.SetActive(true);
            if (Heart1 != null) Heart1.gameObject.SetActive(true);
        }

        lastClickTime = 0f;
    }
}