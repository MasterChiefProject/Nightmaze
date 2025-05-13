using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    [Header("Audio")]
    [Tooltip("Assign your 3 scary death clips here")]
    [SerializeField] private AudioClip[] deathClips;

    [Tooltip("AudioSource on this GameObject (uncheck Play On Awake)")]
    [SerializeField] private AudioSource audioSource;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Globals.playerTag))
        {
            // show cursor
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // play a random death sound, then load scene
            PlayRandomDeathSound();
        }
    }

    private void PlayRandomDeathSound()
    {
        if (deathClips == null || deathClips.Length == 0 || audioSource == null)
        {
            // fallback if not set up correctly
            SceneManager.LoadScene(Globals.deathScene);
            return;
        }

        int idx = Random.Range(0, deathClips.Length);
        AudioClip clip = deathClips[idx];
        audioSource.clip = clip;
        audioSource.Play();

        // start coroutine that waits for the clip to finish
        StartCoroutine(LoadSceneAfterDelay(clip.length));
    }

    private IEnumerator LoadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(Globals.deathScene);
    }
}
