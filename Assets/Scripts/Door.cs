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
        if (other.gameObject.tag == Globals.playerTag) {
            animator.SetBool(Globals.doorOpenParameter, true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == Globals.playerTag)
        {
            animator.SetBool(Globals.doorOpenParameter, false);
        }
    }

}
