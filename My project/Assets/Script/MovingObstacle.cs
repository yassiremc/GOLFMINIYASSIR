using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    public float speed = 40f;
    public float distance = 3f;

    private Vector3 startPos;
    private int direction = 1;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        transform.Translate(Vector3.right * direction * speed * Time.deltaTime);

        if (Mathf.Abs(transform.position.x - startPos.x) >= distance)
        {
            direction *= -1;
        }
    }
}