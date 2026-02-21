using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public float moveDistance = 3f;
    public float moveSpeed = 2f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float offset = Mathf.PingPong(Time.time * moveSpeed, moveDistance * 2) - moveDistance;
        transform.position = new Vector3(startPos.x + offset, startPos.y, startPos.z);
    }
}
