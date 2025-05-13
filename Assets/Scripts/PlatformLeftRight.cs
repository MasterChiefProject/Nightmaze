using UnityEngine;

public class PlatformLeftRight : MonoBehaviour
{
    [Tooltip("Distance to the right of start position")]
    public float x = 3f;
    [Tooltip("Distance to the left of start position")]
    public float y = 2f;
    [Tooltip("Cycles per second")]
    public float speed = 1f;

    Vector3 startPosition;
    float totalDistance;

    void Start()
    {
        startPosition = transform.position;
        totalDistance = x + y;
    }
    void Update()
    {
        // shift object in range [-y,+x]
        float offset = Mathf.PingPong(Time.time * speed + y, totalDistance) - y;
        transform.position = startPosition + Vector3.right * offset;
    }
}
