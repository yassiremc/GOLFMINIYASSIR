using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    [Header("Distancia y altura")]
    public float distance = 8f;
    public float height = 4f;

    [Header("Rotación")]
    public float rotationSpeed = 120f;
    private float currentAngle = 0f;

    [Header("Zoom")]
    public float zoomSpeed = 2f;
    public float minDistance = 4f;
    public float maxDistance = 12f;

    [Header("Suavizado")]
    public float smoothSpeed = 5f;

    [Header("Opcional")]
    public bool onlyFollowWhenMoving = false;

    private Rigidbody rb;

    void Start()
    {
        if (target != null)
            rb = target.GetComponent<Rigidbody>();
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Rotación con ratón (botón derecho)
        if (Input.GetMouseButton(1))
        {
            currentAngle += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        }

        // Zoom con rueda
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        distance -= scroll * zoomSpeed;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        // Dirección de la cámara
        Quaternion rotation = Quaternion.Euler(0, currentAngle, 0);
        Vector3 direction = rotation * new Vector3(0, height, -distance);

        Vector3 desiredPosition = target.position + direction;

        // Seguir siempre o solo cuando se mueve
        if (!onlyFollowWhenMoving || (rb != null && rb.linearVelocity.magnitude > 0.05f))
        {
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        }

        // Mirar a la bola
        transform.LookAt(target.position + Vector3.up * 0.5f);
    }
}