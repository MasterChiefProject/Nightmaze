using UnityEngine;

public class Door : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            animator.SetBool("isOpen", true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            animator.SetBool("isOpen", false);
        }
    }

}
