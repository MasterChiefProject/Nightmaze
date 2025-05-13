using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public GameObject quitMenu;
    public Button playButton;
    public Button exitButton;

    void Start()
    {
        quitMenu.SetActive(false);
    }

    public void PlayButtonPressed()
    {
        // TODO: switch to the first game scene
    }

    public void ExitButtonPressed()
    {
        quitMenu.SetActive(true);
        playButton.enabled = false;
        exitButton.enabled = false;
    }

}
