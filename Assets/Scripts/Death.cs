using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    [Header("Audio")]
    [Tooltip("Assign the scary death clips here.")]
    [SerializeField] private AudioClip[] deathClips;

    [Tooltip("AudioSource on this GameObject (Play On Awake should be disabled).")]
    [SerializeField] private AudioSource audioSource;

    private bool triggered;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered || !other.CompareTag(Globals.playerTag))
        {
            return;
        }

        triggered = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        float delay = PlayRandomDeathSound();
        StartCoroutine(LoadSceneAfterDelay(delay));
    }

    private float PlayRandomDeathSound()
    {
        if (deathClips == null || deathClips.Length == 0 || audioSource == null)
        {
            return 0f;
        }

        AudioClip clip = deathClips[Random.Range(0, deathClips.Length)];
        if (clip == null)
        {
            return 0f;
        }

        audioSource.clip = clip;
        audioSource.Play();
        return clip.length;
    }

    private IEnumerator LoadSceneAfterDelay(float delay)
    {
        if (delay > 0f)
        {
            yield return new WaitForSeconds(delay);
        }

        SceneManager.LoadScene(Globals.deathScene);
    }
}
