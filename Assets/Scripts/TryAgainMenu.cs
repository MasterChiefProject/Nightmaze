using UnityEngine;
using UnityEngine.SceneManagement;

public class TryAgainMenu : MonoBehaviour
{
    public void YesButtonPressed()
    {
        SceneManager.LoadScene(Globals.gameScene);
    }

    public void NoButtonPressed()
    {
        SceneManager.LoadScene(Globals.mainMenuScene);
    }

}
