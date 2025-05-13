using UnityEngine;

public class Portal : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // TODO: move player to another level
        }
    }
}
