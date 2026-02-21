using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("BGM Clips")]
    public AudioClip commonBGM;    
    public AudioClip bossBGM;      
    public AudioClip completeBGM;  

    private AudioSource _src;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            _src = GetComponent<AudioSource>();
            _src.loop = true;
            
            PlayClip(commonBGM);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()  => SceneManager.sceneLoaded += OnSceneLoaded;
    void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string name = scene.name;

        if (name == "Start" ||
            name.StartsWith("Rule") ||
            name.StartsWith("Room"))
        {
            if (_src.clip != commonBGM) PlayClip(commonBGM);
            return;
        }

        if (name == "BossRule" || name == "BossRoom")
        {
            if (_src.clip != bossBGM) PlayClip(bossBGM);
            return;
        }

        if (name == "Complete")
        {
            if (_src.clip != completeBGM) PlayClip(completeBGM);
            return;
        }

    }

    void PlayClip(AudioClip clip)
    {
        if (clip == null) return;
        _src.clip = clip;
        _src.Play();
    }
}
