using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class DoorDoubleClickToRoom1 : MonoBehaviour
{
    public Transform doorObject;         
    public Image whiteFadeImage;         
    public float fadeDuration = 2f;
    public float doorOpenAngle = 90f;
    public string nextSceneName = "Room1";

    private bool hasActivated = false;
    private float lastClickTime = 0f;
    private float clickDelay = 0.3f;

    void OnMouseDown()
    {
        if (hasActivated) return;

        if (Time.time - lastClickTime < clickDelay)
        {
            hasActivated = true;
            StartCoroutine(OpenDoor());
            StartCoroutine(FadeToWhiteAndLoadScene());
        }

        lastClickTime = Time.time;
    }

    IEnumerator OpenDoor()
    {
        float duration = 1f;
        float elapsed = 0f;
        Quaternion startRot = doorObject.rotation;
        Quaternion targetRot = startRot * Quaternion.Euler(0, doorOpenAngle, 0);

        while (elapsed < duration)
        {
            doorObject.rotation = Quaternion.Slerp(startRot, targetRot, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        doorObject.rotation = targetRot;
    }

    IEnumerator FadeToWhiteAndLoadScene()
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
