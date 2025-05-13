using UnityEngine;
using UnityEngine.SceneManagement;
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
        SceneManager.LoadScene(Globals.gameScene);
    }

    public void ExitButtonPressed()
    {
        quitMenu.SetActive(true);
        playButton.enabled = false;
        exitButton.enabled = false;
    }

}
