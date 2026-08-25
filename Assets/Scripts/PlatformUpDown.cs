using UnityEngine;

public class PlatformUpDown : MonoBehaviour
{
    [Tooltip("Distance above the start position")]
    public float up = 3f;

    [Tooltip("Distance below the start position")]
    public float down = 2f;

    [Tooltip("Cycles per second")]
    public float speed = 1f;

    private Vector3 startPosition;
    private float totalDistance;

    private void Start()
    {
        startPosition = transform.position;
        totalDistance = up + down;
    }

    private void Update()
    {
        float offset = Mathf.PingPong(Time.time * speed + down, totalDistance) - down;
        transform.position = startPosition + Vector3.up * offset;
    }
}
