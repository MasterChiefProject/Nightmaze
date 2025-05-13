using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    public void BackButtonPressed()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
