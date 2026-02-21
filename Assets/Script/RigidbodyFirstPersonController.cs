using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RigidbodyFirstPersonController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 10f;

    [Header("Mouse Look")]
    public float mouseSensitivity = 100f;
     
    [Tooltip("반드시 Main Camera의 Transform을 할당하세요")]
    public Transform cameraTransform;

    [Header("Head Bobbing")]
    public float bobSpeed   = 14f;    
    public float bobAmount  = 0.1f; 
    private float defaultCamY;        
    private float bobTimer = 0f;

    private Rigidbody rb;
    private bool     isDragging = false;
    private float    xRotation  = 0f;
    private float    yaw        = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible   = true;

        defaultCamY = cameraTransform.localPosition.y;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) isDragging = true;
        if (Input.GetMouseButtonUp(0))   isDragging = false;

        if (!isDragging) return;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        yaw += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    void LateUpdate()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (input.sqrMagnitude > 0.01f)
        {
            bobTimer += bobSpeed * Time.deltaTime;
            float bobOffset = Mathf.Sin(bobTimer) * bobAmount;
            cameraTransform.localPosition = new Vector3(
                cameraTransform.localPosition.x,
                defaultCamY + bobOffset,
                cameraTransform.localPosition.z
            );
        }
        else
        {
            bobTimer = 0f;
            cameraTransform.localPosition = new Vector3(
                cameraTransform.localPosition.x,
                defaultCamY,
                cameraTransform.localPosition.z
            );
        }
    }

    void FixedUpdate()
    {
        rb.MoveRotation(Quaternion.Euler(0f, yaw, 0f));

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        rb.MovePosition(rb.position + move.normalized * moveSpeed * Time.fixedDeltaTime);
    }
}
