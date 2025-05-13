using UnityEngine;
using UnityEngine.UI;

public class QuitMenu : MonoBehaviour
{

    public Button playButton;
    public Button exitButton;

    public void YesButtonPressed()
    {
        Application.Quit();
    }

    public void NoButtonPressed()
    {
        playButton.enabled = true;
        exitButton.enabled = true;
        gameObject.SetActive(false);
    }
}
