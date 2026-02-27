using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(LineRenderer))]
public class BallShoot : MonoBehaviour
{
    [Header("Fuerza y línea")]
    public float maxForce = 60f;
    public float chargeSpeed = 30f;
    public float maxLineLength = 5f;

    [Header("Parada automática")]
    public float stopThreshold = 0.05f; // velocidad mínima para considerar la bola parada

    private Rigidbody rb;
    private LineRenderer lr;

    private Vector3 direction;
    private float currentForce;
    private bool isCharging = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 2;
        lr.enabled = false;
    }

    void Update()
    {
        // Solo permitir apuntar si la bola está quieta
        if (rb.linearVelocity.magnitude <= stopThreshold)
        {
            Aim();
            HandleInput();
            DrawLine();
        }
        else
        {
            lr.enabled = false; // desactiva la línea mientras se mueve
        }
    }

    void FixedUpdate()
    {
        // Detener la bola si va demasiado despacio
        if (rb.linearVelocity.magnitude < stopThreshold && rb.linearVelocity.magnitude != 0f)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    void Aim()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, transform.position);

        if (plane.Raycast(ray, out float distance))
        {
            Vector3 hitPoint = ray.GetPoint(distance);
            direction = (transform.position - hitPoint).normalized;
        }
    }

    void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isCharging = true;
            currentForce = 0f;
            lr.enabled = true;
        }

        if (Input.GetMouseButton(0) && isCharging)
        {
            currentForce += Time.deltaTime * chargeSpeed;
            currentForce = Mathf.Clamp(currentForce, 0, maxForce);
        }

        if (Input.GetMouseButtonUp(0) && isCharging)
        {
            Shoot();
            isCharging = false;
            lr.enabled = false;
        }
    }

  void Shoot()
{
    rb.AddForce(direction * currentForce * 4f, ForceMode.Impulse);
}

    void DrawLine()
    {
        if (!lr.enabled) return;

        Vector3 start = transform.position;
        float lineLength = (currentForce / maxForce) * maxLineLength;
        Vector3 end = start + direction * lineLength;

        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
    }
}