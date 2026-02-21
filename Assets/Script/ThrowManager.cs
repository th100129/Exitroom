using UnityEngine;

public class ThrowManager : MonoBehaviour
{
    public GameObject[] throwPrefabs; 
    public Transform throwPoint;      
    public float throwForce = 700f;
    public bool isEnabled = true;     

    void Update()
    {
        if (!isEnabled)
        {
            Debug.Log("ThrowManager: 던지기 비활성화 상태입니다.");
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("ThrowManager: 마우스 클릭 감지됨, 던지기 시도");

            ThrowRandomObject();
        }
    }

    void ThrowRandomObject()
    {
        if (throwPrefabs.Length == 0)
        {
            Debug.LogWarning("⚠️ ThrowManager: throwPrefabs 배열이 비어 있습니다.");
            return;
        }

        int index = Random.Range(0, throwPrefabs.Length);
        GameObject prefab = throwPrefabs[index];

        GameObject obj = Instantiate(prefab, throwPoint.position, Quaternion.identity);
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb == null) rb = obj.AddComponent<Rigidbody>();

        Vector3 dir = GetMouseWorldPosition() - throwPoint.position;
        rb.AddForce(dir.normalized * throwForce);
    }

    Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return ray.GetPoint(10f);
    }
}
