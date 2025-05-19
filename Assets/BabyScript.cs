using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BabyScript : MonoBehaviour
{
    [Tooltip("Assign a baby sound AudioClip here")]
    public AudioClip babyCryClip;

    private AudioSource audioSource;

    void Awake()
    {
        // Grab (or add) the AudioSource on this GameObject
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the thing that hit us is tagged "Player"
        if (other.CompareTag("Player"))
        {
            // Play the cry clip once
            if (babyCryClip != null)
            {
                audioSource.PlayOneShot(babyCryClip);
            }
            else
            {
                Debug.LogWarning("BabyScript: No babyCryClip assigned!", this);
            }
        }
    }
}
