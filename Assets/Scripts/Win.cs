using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(Globals.playerTag))
        {
            return;
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(Globals.winScene);
    }
}
