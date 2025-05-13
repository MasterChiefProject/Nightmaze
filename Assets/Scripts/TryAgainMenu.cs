using UnityEngine;
using UnityEngine.SceneManagement;

public class TryAgainMenu : MonoBehaviour
{


    public void YesButtonPressed()
    {
        // TODO: go back to the game scene from the beginning
    }

    public void NoButtonPressed()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

}
