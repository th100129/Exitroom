using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Timer Settings")]
    public float timeLimit = 20f;
    private float currentTime;
    private bool timerActive = true;

    [Header("UI")]
    public Image whiteFade;
    public TextMeshProUGUI timerText;

    [Header("Scene & Boss")]
    public string failScene = "Room0";
    public BossHealth bossHealth;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currentTime = timeLimit;

        if (whiteFade != null)
        {
            Color c = whiteFade.color;
            c.a = 0f;
            whiteFade.color = c;
            whiteFade.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (!timerActive) return;

        currentTime -= Time.deltaTime;

        if (timerText != null)
        {
            timerText.text = Mathf.Ceil(currentTime).ToString("0");
        }

        if (bossHealth == null || !bossHealth.gameObject.activeSelf || bossHealth.currentHealth <= 0f)
        {
            timerActive = false;
            if (timerText != null)
                timerText.text = "";
            return;
        }

        if (currentTime <= 0f)
        {
            timerActive = false;
            StartCoroutine(FadeAndLoadRoom0());
        }
    }

    IEnumerator FadeAndLoadRoom0()
    {
        whiteFade.gameObject.SetActive(true);
        Color color = whiteFade.color;

        for (float t = 0; t < 1f; t += Time.deltaTime)
        {
            color.a = t;
            whiteFade.color = color;
            yield return null;
        }

        SceneManager.LoadScene(failScene);
    }
}
