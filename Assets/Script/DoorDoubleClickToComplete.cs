using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DoorDoubleClickToComplete : MonoBehaviour
{
    public Image whiteFade;             
    public string nextScene = "Complete";  
    public float fadeSpeed = 1f;        

    private float lastClickTime = 0f;   
    private float clickDelay = 0.3f;    
    private bool isFading = false;

    void OnMouseDown()
    {
        if (Time.time - lastClickTime < clickDelay && !isFading)
        {
            isFading = true;
            StartCoroutine(FadeOutAndLoadScene());
        }
        lastClickTime = Time.time;
    }

    System.Collections.IEnumerator FadeOutAndLoadScene()
    {
        whiteFade.gameObject.SetActive(true);
        Color color = whiteFade.color;
        float alpha = 0f;

        while (alpha < 1f)
        {
            alpha += Time.deltaTime * fadeSpeed;
            color.a = alpha;
            whiteFade.color = color;
            yield return null;
        }

        SceneManager.LoadScene(nextScene);
    }
}
