using UnityEngine;
using UnityEngine.UI;

public class QuitMenu : MonoBehaviour
{
    public Button playButton;
    public Button exitButton;

    public void YesButtonPressed()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        Debug.Log("Browser tabs cannot be closed by a Unity WebGL player. Returning to the main menu.");
        RestoreMainMenu();
#else
        Application.Quit();
#endif
    }

    public void NoButtonPressed()
    {
        RestoreMainMenu();
    }

    private void RestoreMainMenu()
    {
        playButton.interactable = true;
        exitButton.interactable = true;
        gameObject.SetActive(false);
    }
}
