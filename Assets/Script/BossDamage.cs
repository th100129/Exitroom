using UnityEngine;

public class BossDamage : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Throwable"))
        {
            Destroy(collision.gameObject);
           
        }
    }
}
