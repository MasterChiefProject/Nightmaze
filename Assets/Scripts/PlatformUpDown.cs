using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlatformUpDown : MonoBehaviour
{
    [Tooltip("Distance above the start position")]
    public float up = 3f;
    [Tooltip("Distance below the start position")]
    public float down = 2f;
    [Tooltip("Cycles per second")]
    public float speed = 1f;

    Vector3 startPosition;
    float totalDisttance;

    void Start()
    {
        startPosition = transform.position;
        totalDisttance = up + down;
    }

    void Update()
    {
        float offset = Mathf.PingPong(Time.time * speed + down, totalDisttance) - down;
        transform.position = startPosition + Vector3.up * offset;
    }
}
