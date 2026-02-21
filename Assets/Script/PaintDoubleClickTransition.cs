using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class PaintDoubleClickTransition : MonoBehaviour
{
    public Image whiteFadeImage;        
    public float fadeDuration = 2f;     
    public string nextSceneName = "Room3";

    private float lastClickTime = 0f;
    private float doubleClickDelay = 0.3f;
    private bool isTransitioning = false;

    void OnMouseDown()
    {
        if (isTransitioning) return;

        if (Time.time - lastClickTime < doubleClickDelay)
        {
            isTransitioning = true;
            StartCoroutine(FadeAndLoad());
        }

        lastClickTime = Time.time;
    }

    IEnumerator FadeAndLoad()
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
        SceneManager.LoadScene(nextSceneName);
    }
}
