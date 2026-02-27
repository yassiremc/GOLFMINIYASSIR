using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(LineRenderer))]
public class LineForce : MonoBehaviour
{
    public float maxForce = 20f;
    public float forceMultiplier = 10f;
    public int linePoints = 20;
    public float lineLength = 2f;

    private Rigidbody rb;
    private LineRenderer lr;

    private Vector3 direction;
    private float currentForce;
    private bool isCharging = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lr = GetComponent<LineRenderer>();
        lr.positionCount = linePoints;
        lr.enabled = false;
    }

    void Update()
    {
        Aim();
        HandleInput();
        DrawLine();
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
            currentForce += Time.deltaTime * forceMultiplier;
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
        rb.AddForce(direction * currentForce, ForceMode.Impulse);
    }

    void DrawLine()
    {
        if (!lr.enabled) return;

        Vector3 startPoint = transform.position;

        for (int i = 0; i < linePoints; i++)
        {
            float t = i / (float)linePoints;
            Vector3 point = startPoint + direction * (t * lineLength * currentForce / maxForce);
            lr.SetPosition(i, point);
        }
    }
}