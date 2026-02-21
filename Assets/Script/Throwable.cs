using UnityEngine;

public class Throwable : MonoBehaviour
{
    public float damage = 20f;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Boss"))
        {
            BossHealth boss = collision.gameObject.GetComponent<BossHealth>();
            if (boss != null)
            {
                boss.TakeDamage(damage);
            }
        }

        Destroy(gameObject); 
    }
}
