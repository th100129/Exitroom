using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    [Header("Health")]
    public float maxHealth = 100f;
    public float currentHealth;
    public Image healthBarFill;

    [Header("References")]
    public GameObject playerControllerObject;
    public ThrowManager throwManager;
    public GameObject bossHealthBarUIObject;
    public ParticleSystem explosionEffect;

    [Header("Audio")]
    public AudioClip damageSound;     
    private AudioSource audioSource;   

    private Transform bossTransform;
    private Vector3 originalScale;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();

        if (playerControllerObject != null)
            playerControllerObject.GetComponent<RigidbodyFirstPersonController>().enabled = false;

        bossTransform = transform;
        originalScale = bossTransform.localScale;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    public void TakeDamage(float amount)
    {
        
        if (damageSound != null)
            audioSource.PlayOneShot(damageSound);

       
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        UpdateHealthBar();
        ShrinkBoss();

       
        if (currentHealth <= 0f)
            Die();
    }

    void UpdateHealthBar()
    {
        if (healthBarFill != null)
            healthBarFill.fillAmount = currentHealth / maxHealth;

        if (currentHealth <= 0f && bossHealthBarUIObject != null)
            bossHealthBarUIObject.SetActive(false);
    }

    void ShrinkBoss()
    {
        if (bossTransform != null)
        {
            float shrinkFactor = currentHealth / maxHealth;
            bossTransform.localScale = originalScale * shrinkFactor;
        }
    }

    void Die()
    {
        Debug.Log("Boss defeated!");

        if (explosionEffect != null)
        {
            ParticleSystem ps = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            ps.Play();
            Destroy(ps.gameObject, 3f);
        }

        gameObject.SetActive(false);

        if (playerControllerObject != null)
            playerControllerObject.GetComponent<RigidbodyFirstPersonController>().enabled = true;

        if (throwManager != null)
        {
            throwManager.isEnabled = false;
            Debug.Log("Throwing disabled!");
        }

        if (bossHealthBarUIObject != null)
            bossHealthBarUIObject.SetActive(false);
    }
}
