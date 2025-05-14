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

            //play a random death sound
            float delay = PlayRandomDeathSound();

            //load death scene
            StartCoroutine(LoadSceneAfterDelay(delay));
        }
    }

    private float PlayRandomDeathSound()
    {
        return 0f;

        if (deathClips == null || deathClips.Length == 0 || audioSource == null)
            return 0f;

        int idx = Random.Range(0, deathClips.Length);
        AudioClip clip = deathClips[idx];
        audioSource.clip = clip;
        audioSource.Play();

        return clip.length;
    }

    private IEnumerator LoadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        LoadDeahScene();
    }

    private void LoadDeahScene()
    {
        SceneManager.LoadScene(Globals.deathScene);
    }
}
