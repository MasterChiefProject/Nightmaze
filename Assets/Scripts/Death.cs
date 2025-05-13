using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Globals.playerTag)
        {
            SceneManager.LoadScene(Globals.deathScene);
        }
    }

}
