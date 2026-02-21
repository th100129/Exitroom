using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LaptopClickTransition : MonoBehaviour
{
    public Image whiteFade;  
    public string nextSceneName = "Room2";
    private float lastClickTime = 0f;
    private float clickDelay = 0.3f;

    void OnMouseDown()
    {
        if (Time.time - lastClickTime < clickDelay)
        {
            StartCoroutine(FadeAndLoad());
        }
        lastClickTime = Time.time;
    }

    System.Collections.IEnumerator FadeAndLoad()
    {
        whiteFade.gameObject.SetActive(true);
        Color color = whiteFade.color;
        for (float t = 0; t < 1; t += Time.deltaTime)
        {
            color.a = t;
            whiteFade.color = color;
            yield return null;
        }
        SceneManager.LoadScene(nextSceneName);
    }
}
